using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm
{
    public class Module
    {
        private string wat = "";
        public string ToWebassemblyText() => $"(module {_function.Signature} {_function})";

        private Function _function;

        public Signature AddFunctionType(string name, ValueType returnType, ValueType[] parameters)
        {
            var signature = new Signature(name, returnType, parameters);
            return signature;
        }

        public Expression GetLocal(int index, ValueType type)
        {
            return new Expression(index,type);
        }

        public Expression Binary(BinaryOperator op, Expression left, Expression right)
        {
            return new Expression(op,left,right);
        }

        public Expression Binary(BinaryOperator op, params Expression[] expressions)
        {
            return new Expression(op, expressions);
        }

        public void AddFunction(string name, Signature signature, Expression expression)
        {
            var function = new Function(name, signature, expression);
            _function = function;
        }
    }
}
