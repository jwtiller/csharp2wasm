using System.Text.RegularExpressions;
using csharp2wasm;
using NUnit.Framework;

namespace unittests
{
    public class LocalFunctionTests
    {
        private readonly Csharp2WasmConverter _converter = new();

        [Test]
        public void BasicAddTwoIntTest_ShouldReturnExpectedWatCode()
        {
            var module = new Module();
            var parameters = new [] { ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);

       
            var x = module.GetLocal(0, ValueType.Int32);
            var y = module.GetLocal(1, ValueType.Int32);
            var add = module.Binary(BinaryOperator.AddInt32, x, y);

            
            var adder = module.AddFunction("adder", signature, add);
            var wat = module.ToWat();
            Assert.AreEqual(Regex.Unescape("(module (type $add (func (param i32 i32) (result i32))) (func $adder (; 0 ;) (type $add) (param $0 i32) (param $1 i32) (result i32) (i32.add (get_local $0) (get_local $1))))"),wat);
        }

        [Test]
        public void BasicAddThreeIntTest_ShouldReturnExpectedWatCode()
        {
            var module = new Module();
            var parameters = new[] { ValueType.Int32, ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);

      
            var x = module.GetLocal(0, ValueType.Int32);
            var y = module.GetLocal(1, ValueType.Int32);
            var z = module.GetLocal(2, ValueType.Int32);
            var add = module.Binary(BinaryOperator.AddInt32, x, y, z);

            var adder = module.AddFunction("adder", signature, add);
            var wat = module.ToWat();
            Assert.AreEqual(Regex.Unescape("(module (type $add (func (param i32 i32 i32) (result i32))) (func $adder (; 0 ;) (type $add) (param $0 i32) (param $1 i32) (param $2 i32) (result i32) (i32.add (get_local $0) (get_local $1) (get_local $2))))"), wat);
        }

        [Test]
        public void BasicMultiplyTwoIntTest_ShouldReturnExpectedWatCode()
        {
            var module = new Module();
            var parameters = new[] { ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("multiply", ValueType.Int32, parameters);

            var x = module.GetLocal(0, ValueType.Int32);
            var y = module.GetLocal(1, ValueType.Int32);
            var add = module.Binary(BinaryOperator.MulInt32, x, y);

            var adder = module.AddFunction("multiplyer", signature, add);
            var wat = module.ToWat();
            Assert.AreEqual(Regex.Unescape("(module (type $multiply (func (param i32 i32) (result i32))) (func $multiplyer (; 0 ;) (type $multiply) (param $0 i32) (param $1 i32) (result i32) (i32.mul (get_local $0) (get_local $1))))"), wat);
        }

        [Test]
        public void BasicSubTwoIntTest_ShouldReturnExpectedWatCode()
        {
            var module = new Module();
            var parameters = new[] { ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("sub", ValueType.Int32, parameters);

            var x = module.GetLocal(0, ValueType.Int32);
            var y = module.GetLocal(1, ValueType.Int32);
            var sub = module.Binary(BinaryOperator.SubInt32, x, y);

            var adder = module.AddFunction("subtract", signature, sub);
            var wat = module.ToWat();
            Assert.AreEqual(Regex.Unescape("(module (type $sub (func (param i32 i32) (result i32))) (func $subtract (; 0 ;) (type $sub) (param $0 i32) (param $1 i32) (result i32) (i32.sub (get_local $0) (get_local $1))))"), wat);
        }
    }
}