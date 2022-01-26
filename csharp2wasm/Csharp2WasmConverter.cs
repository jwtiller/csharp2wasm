using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace csharp2wasm
{
    public class Csharp2WasmConverter : CSharpSyntaxWalker
    {
        private Module _module = new();
        public string Transpile(string csharp)
        {
            _module = new();

            var tree = CSharpSyntaxTree.ParseText(csharp);
            var root = tree.GetRoot();
            Visit(root);

            return _module.ToWebassemblyText();
        }


        public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
        {
        }

    }
}