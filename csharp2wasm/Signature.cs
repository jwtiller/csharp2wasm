using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm
{
    public class Signature
    {
        public string Name { get; set; }
        public ValueType ReturnType { get; set; }
        public ValueType[] Parameters { get; set; }

        public Signature(string name, ValueType returnType, ValueType[] parameters)
        {
            Name = name;
            ReturnType = returnType;
            Parameters = parameters;
        }

        public override string ToString()
        {
            return $"(type ${Name} (func (param {string.Join(' ', ValueTypeStringConverter.Convert(Parameters))}) (result {ValueTypeStringConverter.Convert(ReturnType)})))";
        }
    }
}
