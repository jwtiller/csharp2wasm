using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm
{
    public class Function
    {
        public Function(string name, Signature signature, Expression expression)
        {
            Name = name;
            Signature = signature;
            Expression = expression;
        }

        public string Name { get; }
        public Signature Signature { get; }
        public Expression Expression { get; }


        public override string ToString()
        {
            var parameters = string.Empty;
            for (int i = 0; i < Signature.Parameters.Length; i++)
            {
                parameters += $" (param ${i} {ValueTypeStringConverter.Convert(Signature.Parameters[i])})";
            }
            var function = $"(type ${Signature.Name}) {parameters.Trim()} (result {ValueTypeStringConverter.Convert(Signature.ReturnType)})";
            return $"(func ${Name} (; 0 ;) {function} {Expression}";
        }
    }
}
