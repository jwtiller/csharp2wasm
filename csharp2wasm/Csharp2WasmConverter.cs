using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace csharp2wasm
{
    public class Csharp2WasmConverter : CSharpSyntaxWalker
    {
        private readonly StringBuilder output = new();
        public string Transpile(string csharp)
        {
            output.Clear();
            AddOutputCode("(module");

            var tree = CSharpSyntaxTree.ParseText(csharp);
            var root = tree.GetRoot();
            Visit(root);

            AddOutputCode(")");
            return output.ToString();
        }


        public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
        {
            var name = node.Identifier.Text;

            var parameters = node.ParameterList.Parameters
                .Select(x => Map(x.Type.ToString()))
                .ToArray();
            AddOutputCode($" (func (export \"{name}\") (param {string.Join(" ",parameters)})");

            var returnType = Map(node.ReturnType.ToString());
            AddOutputCode($" (result {returnType})");

            for (int i = 0; i < parameters.Length; i++)
            {
                AddOutputCode($" local.get {i}");
            }

            for (int i = 0; i < parameters.Length-1; i++)
            {
                AddOutputCode(" i32.add");
            }
            AddOutputCode(")");
        }


        private readonly List<(string csharpKind, string wasmKind)> _mapping = new()
        {
            ("int", "i32")
        };

        private string Map(string csharpKind) => _mapping.FirstOrDefault(x => x.csharpKind == csharpKind).wasmKind; 


        private void AddOutputCode(string code)
        {
            output.Append(code);
        }
    }
}