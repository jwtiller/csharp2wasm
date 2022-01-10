using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm
{
    public record WasmSyntaxNode
    {
        public NodeType Type { get; set; }
        public string Value { get; set; }
        public List<WasmSyntaxNode> Parents { get; set; }
        public List<WasmSyntaxNode> Children { get; set; }
    }
}
