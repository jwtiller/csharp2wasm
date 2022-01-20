using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm
{
    public static class ExtensionMethods
    {
        public static string ToWatString(this BinaryOperator binaryOperator)
        {
            return binaryOperator switch
            {
                BinaryOperator.AddInt32 => "i32.add",
                BinaryOperator.SubInt32 => "i32.sub",
                BinaryOperator.MulInt32 => "i32.mul",
                _ => throw new NotImplementedException()
            };
        }
    }
}
