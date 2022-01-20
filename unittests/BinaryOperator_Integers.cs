using System;
using System.Text.RegularExpressions;
using csharp2wasm;
using NUnit.Framework;
using ValueType = csharp2wasm.ValueType;

namespace unittests
{
    public class BinaryOperator_Integers
    {
        [TestCaseSource(nameof(BinaryOperators))]
        public void TwoIntegersTest_ShouldReturnExpectedWatCode(BinaryOperator binaryOperator)
        {
            var module = new Module();
            var parameters = new [] { ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);

       
            var x = module.GetLocal(0, ValueType.Int32);
            var y = module.GetLocal(1, ValueType.Int32);
            var add = module.Binary(binaryOperator, x, y);

            
            module.AddFunction("mathFunc", signature, add);
            var wat = module.ToWat();
            Assert.AreEqual(Regex.Unescape($"(module (type $add (func (param i32 i32) (result i32))) (func $mathFunc (; 0 ;) (type $add) (param $0 i32) (param $1 i32) (result i32) ({binaryOperator.ToWatString()} (get_local $0) (get_local $1))))"),wat);
        }

        [TestCaseSource(nameof(BinaryOperators))]
        public void ThreeIntegersTest_ShouldReturnExpectedWatCode(BinaryOperator binaryOperator)
        {
            var module = new Module();
            var parameters = new[] { ValueType.Int32, ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);

      
            var x = module.GetLocal(0, ValueType.Int32);
            var y = module.GetLocal(1, ValueType.Int32);
            var z = module.GetLocal(2, ValueType.Int32);
            var add = module.Binary(binaryOperator, x, y, z);

            module.AddFunction("mathFunc", signature, add);
            var wat = module.ToWat();
            Assert.AreEqual(Regex.Unescape($"(module (type $add (func (param i32 i32 i32) (result i32))) (func $mathFunc (; 0 ;) (type $add) (param $0 i32) (param $1 i32) (param $2 i32) (result i32) ({binaryOperator.ToWatString()} (get_local $0) (get_local $1) (get_local $2))))"), wat);
        }

        [TestCaseSource(nameof(BinaryOperators))]
        public void FourIntegersTest_ShouldReturnExpectedWatCode(BinaryOperator binaryOperator)
        {
            var module = new Module();
            var parameters = new[] { ValueType.Int32, ValueType.Int32, ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);


            var w = module.GetLocal(0, ValueType.Int32);
            var x = module.GetLocal(1, ValueType.Int32);
            var y = module.GetLocal(2, ValueType.Int32);
            var z = module.GetLocal(3, ValueType.Int32);
            var add = module.Binary(binaryOperator, w,x, y, z);

            module.AddFunction("mathFunc", signature, add);
            var wat = module.ToWat();
            Assert.AreEqual(Regex.Unescape($"(module (type $add (func (param i32 i32 i32 i32) (result i32))) (func $mathFunc (; 0 ;) (type $add) (param $0 i32) (param $1 i32) (param $2 i32) (param $3 i32) (result i32) ({binaryOperator.ToWatString()} (get_local $0) (get_local $1) (get_local $2) (get_local $3))))"), wat);
        }

        [TestCaseSource(nameof(BinaryOperators))]
        public void FiveIntegersTest_ShouldReturnExpectedWatCode(BinaryOperator binaryOperator)
        {
            var module = new Module();
            var parameters = new[] { ValueType.Int32, ValueType.Int32, ValueType.Int32, ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);

            var v = module.GetLocal(0, ValueType.Int32);
            var w = module.GetLocal(1, ValueType.Int32);
            var x = module.GetLocal(2, ValueType.Int32);
            var y = module.GetLocal(3, ValueType.Int32);
            var z = module.GetLocal(4, ValueType.Int32);
            var add = module.Binary(binaryOperator, v,w, x, y, z);

            module.AddFunction("mathFunc", signature, add);
            var wat = module.ToWat();
            Assert.AreEqual(Regex.Unescape($"(module (type $add (func (param i32 i32 i32 i32 i32) (result i32))) (func $mathFunc (; 0 ;) (type $add) (param $0 i32) (param $1 i32) (param $2 i32) (param $3 i32) (param $4 i32) (result i32) ({binaryOperator.ToWatString()} (get_local $0) (get_local $1) (get_local $2) (get_local $3) (get_local $4))))"), wat);
        }

        private static BinaryOperator[] BinaryOperators => Enum.GetValues<BinaryOperator>();
    }
}