# csharp2wasm

## why?
- learn new things
- easy way to convert c# class libraries to .wasm file
  - security: securely isolate untrusted code
  - portability: can be consumed in console program, other programming languages or web.
 
## how (assumptions so far)?
- roslyn compiler api to analyze c# code
- convert to webassembly text format(wat)
- use wat2wasm cli to compile to .wasm file
- autogenerate c# bindings to wasmtime
