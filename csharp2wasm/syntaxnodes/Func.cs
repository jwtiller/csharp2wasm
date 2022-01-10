using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm.syntaxnodes
{
    public record Func : WasmSyntaxNode
    {
        public override string ToString() => "func";
    }
}
