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
        public Expression[] Expressions => new[] { Left, Right };


        private void MergeExpressions(BinaryOperator op,Expression[] expressions)
        {
            Op = op;
            Left = new Expression(op, expressions[0], expressions[1]);
            Right = new Expression(op, null, expressions[2]);
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

            var subExpressions = Expressions.SelectMany(x => x.Expressions).Count(x => x != null);
            var result = new StringBuilder();
            result.Append($"(i32{op}");
            if (subExpressions > 0)
            {
                for (int i = 0; i < subExpressions; i++)
                {
                    result.Append($" (get_local ${i})");
                }
            }
            else
            {
                result.Append(" (get_local $0) (get_local $1)");
            }

            result.Append("))");
            return result.ToString();
        }

    }
}
