using System.Runtime.CompilerServices;
using System.Text;
using csharp2wasm.syntaxnodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace csharp2wasm
{
    public class Csharp2WasmConverter : CSharpSyntaxWalker
    {
        private readonly List<WasmSyntaxNode> _tree = new();
        public string Transpile(string csharp)
        {
            _tree.Clear();
            AddToSyntaxTree(new LeftRoundBracket());
            AddToSyntaxTree(new Module());

            var tree = CSharpSyntaxTree.ParseText(csharp);
            var root = tree.GetRoot();
            Visit(root);

            AddToSyntaxTree(new RightRoundBracket());

            return ConvertTreeToString(_tree);
        }

        private string ConvertTreeToString(List<WasmSyntaxNode> tree)
        {
            var output = new StringBuilder();
            foreach (var node in tree)
            {
                string trailingSpace = node.TrailingSpace ? " " : string.Empty;
                output.Append($"{node.ToString()}{trailingSpace}");
            }

            return output.ToString();
        }


        public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
        {
            var name = node.Identifier.Text;
            var operators = GetOperators(node.DescendantNodes().OfType<ReturnStatementSyntax>()).ToList();
            var parameters = node.ParameterList.Parameters
                .Select(x => Map(x.Type.ToString()))
                .ToArray();
            AddToSyntaxTree(new LeftRoundBracket());
            AddToSyntaxTree(new Func());
            AddToSyntaxTree(new LeftRoundBracket());
            AddToSyntaxTree(new Export() { Value = name });
            AddToSyntaxTree(new RightRoundBracket() { TrailingSpace = true});
            AddToSyntaxTree(new Param() { Value = string.Join(" ", parameters) });

            var returnType = Map(node.ReturnType.ToString());
            AddToSyntaxTree(new Result() { Value = returnType });

            for (int i = 0; i < parameters.Length; i++)
            {
                AddToSyntaxTree(new LocalGet() { Value = i.ToString() });
            }

            for (int i = 0; i < parameters.Length-1; i++)
            {
                bool trailingSpace = i < parameters.Length-2;
                Operator? expressionOp = null;
                if (i < operators.Count)
                {
                    expressionOp = operators[i];
                }

                AddToSyntaxTree(new I32() { Operator = expressionOp, TrailingSpace = trailingSpace });
            }
            AddToSyntaxTree(new RightRoundBracket());
        }

        private IEnumerable<Operator> GetOperators(IEnumerable<ReturnStatementSyntax> statements)
        {
            foreach (var statement in statements)
            {
                if (statement.Expression is BinaryExpressionSyntax expression)
                {
                    var left = expression.Left.Kind();
                    if (left == SyntaxKind.AddExpression)
                        yield return Operator.Add;
                    if (left == SyntaxKind.SubtractExpression)
                        yield return Operator.Sub;
                    if (left == SyntaxKind.MultiplyExpression)
                        yield return Operator.Mul;

                    yield return expression.OperatorToken.Value switch
                    {
                        "+" => Operator.Add,
                        "-" => Operator.Sub,
                        "*" => Operator.Mul
                    };
                }
     
            }
        }

        private string Map(string type)
        {
            return type switch
            {
                "int" => "i32",
                _ => string.Empty
            };
        }

        private void AddToSyntaxTree(WasmSyntaxNode node)
        {
            _tree.Add(node);
        }
    }
}