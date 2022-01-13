using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm
{
    public static class ValueTypeStringConverter
    {
        public static string Convert(ValueType valueType)
        {
            return valueType switch
            {
                ValueType.Int32 => "i32",
                _ => string.Empty
            };
        }
        public static string Convert(ValueType[] valueTypes)
        {
            var result = new List<string>();
            foreach (var valueType in valueTypes)
            {
                result.Add(Convert(valueType));
            }
            return string.Join(' ',result);
        }
    }
}
