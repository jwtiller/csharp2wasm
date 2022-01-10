using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm.syntaxnodes
{
    public record RightRoundBracket : WasmSyntaxNode
    {
        public override string ToString() => ")";
        public override bool TrailingSpace { get; set; } = false;
    }
}
