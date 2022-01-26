using System.Text.RegularExpressions;
using NUnit.Framework;
using ValueType = csharp2wasm.ValueType;

namespace unittests.BinaryOperatorTests.Subtract
{
    [TestFixture]
    public class SubtractFiveIntegersTests : BinaryOperatorTest
    {

        [SetUp]
        public void Setup()
        {
            var module = new csharp2wasm.Module();
            var parameters = new[] { csharp2wasm.ValueType.Int32, ValueType.Int32, ValueType.Int32, ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);

            var v = module.GetLocal(0, ValueType.Int32);
            var w = module.GetLocal(1, ValueType.Int32);
            var x = module.GetLocal(2, ValueType.Int32);
            var y = module.GetLocal(3, ValueType.Int32);
            var z = module.GetLocal(4, ValueType.Int32);
            var add = module.Binary(csharp2wasm.BinaryOperator.SubInt32, v,w, x, y, z);


            module.AddFunction("mathFunc", signature, add);
            WebassemblyText = module.ToWebassemblyText();
        }

        [Test]
        public void AssertExpectedWebassemblyText()
        {
            Assert.AreEqual(
                Regex.Unescape(
                    $"(module (type $add (func (param i32 i32 i32 i32 i32) (result i32))) (export \"mathFunc\" (func $module/mathFunc)) (func $module/mathFunc (type $add) (param $0 i32) (param $1 i32) (param $2 i32) (param $3 i32) (param $4 i32) (result i32) (i32.sub (get_local $0) (get_local $1) i32.sub (get_local $2) i32.sub (get_local $3) i32.sub (get_local $4))))"),
                WebassemblyText);
        }

        [Test]
        public void ShouldReturn_Seven()
        {
            var result = ExecuteWasmCode("add", "mathFunc", -10, -20, 1, 1, 1);
            Assert.AreEqual(7, result);
        }
    }
}
