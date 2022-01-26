using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp2wasm
{
    public class Expression
    {
        public Expression(int index, ValueType returnType)
        {
            Index = index;
        }

        public Expression(BinaryOperator op, Expression left, Expression right)
        {
            Op = op;
            Left = left;
            Right = right;
        }

        public Expression(BinaryOperator op, Expression[] expressions) => MergeExpressions(op, expressions);

        public int Index { get; set; }
        public BinaryOperator? Op { get; set; }
        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public Expression[] Expressions { get; set; }


        private void MergeExpressions(BinaryOperator op,Expression[] expressions)
        {
            Op = op;
            Expressions = expressions;
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

            var result = new StringBuilder();
            result.Append($"(i32{op}");

            int expressions = GetExpressionsSize();
            for (int j = 0; j < expressions; j++)
            {
                if (j > 1)
                {
                    result.Append($" i32{op}");
                }
                result.Append($" (get_local ${j})");
            }

            result.Append("))");
            return result.ToString();
        }


        private int GetExpressionsSize()
        {
            if (Expressions?.Length > 0)
                return Expressions.Length;

            if (Left != null || Right != null)
                return 2;

            return 1;
        }

    }
}
