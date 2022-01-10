using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm.syntaxnodes
{
    public record I32 : WasmSyntaxNode
    {
        public Operator? Operator { get; set; }
        public override string ToString() => $"i32{_extra}";
        private string _extra => Operator != null ? $".{Operator.ToString().ToLower()}" : string.Empty;
    }
}
