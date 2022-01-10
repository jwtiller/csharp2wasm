using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm
{
    public enum NodeType
    {
        Unknown,
        LeftRoundBracket,
        RightRoundBracket,
        Module,
        Func,
        Export,
        Param,
        I32,
        I32Add,
        LocalGet,
        Result
    }
}
