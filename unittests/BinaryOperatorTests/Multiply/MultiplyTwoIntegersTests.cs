using System.Text.RegularExpressions;
using NUnit.Framework;
using ValueType = csharp2wasm.ValueType;

namespace unittests.BinaryOperatorTests.Multiply
{
    [TestFixture]
    public class MultiplyTwoIntegersTests : BinaryOperatorTest
    {
        [SetUp]
        public void Setup()
        {
            var module = new csharp2wasm.Module();
            var parameters = new[] { csharp2wasm.ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);


            var x = module.GetLocal(0, ValueType.Int32);
            var y = module.GetLocal(1, ValueType.Int32);
            var add = module.Binary(csharp2wasm.BinaryOperator.MulInt32, x, y);


            module.AddFunction("mathFunc", signature, add);
            WebassemblyText = module.ToWebassemblyText();
        }

        [Test]
        public void AssertExpectedWebassemblyText()
        {
            Assert.AreEqual(Regex.Unescape($"(module (type $add (func (param i32 i32) (result i32))) (export \"mathFunc\" (func $module/mathFunc)) (func $module/mathFunc (type $add) (param $0 i32) (param $1 i32) (result i32) (i32.mul (get_local $0) (get_local $1))))"), WebassemblyText);
        }

        [Test]
        public void ShouldReturn_ThirtySix()
        {
            var result = ExecuteWasmCode("add", "mathFunc", 6, 6);
            Assert.AreEqual(36, result);
        }
    }
}
