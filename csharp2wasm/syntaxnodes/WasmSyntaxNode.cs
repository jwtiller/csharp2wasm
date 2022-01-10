namespace csharp2wasm.syntaxnodes
{
    public abstract record WasmSyntaxNode
    {
        public string Value { get; set; }
        public List<WasmSyntaxNode> Parents { get; set; }
        public List<WasmSyntaxNode> Children { get; set; }
        public virtual bool TrailingSpace { get; set; } = true;
    }
}
