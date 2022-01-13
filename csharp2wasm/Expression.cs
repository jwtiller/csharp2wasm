using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm
{
    public class Expression
    {
        public Expression(int index, ValueType type)
        {
            Index = index;
            Type = type;
        }

        public Expression(BinaryOperator op, Expression left, Expression right)
        {
            Op = op;
            Left = left;
            Right = right;
        }

        public Expression(BinaryOperator op, Expression[] expressions) => MergeExpressions(op, expressions);

        public int Index { get; set; }
        public ValueType Type { get; set; }
        public BinaryOperator? Op { get; set; }
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        private void MergeExpressions(BinaryOperator op,Expression[] expressions)
        {
            Op = op;
        }

        public override string ToString()
        {
            var op = Op switch
            {
                BinaryOperator.AddInt32 => ".add",
                BinaryOperator.SubInt32 => ".sub",
                BinaryOperator.MulInt32 => ".mul",
                _ => string.Empty
            };
            return $"(i32{op} (get_local $0) (get_local $1)))";
        }

    }
}
