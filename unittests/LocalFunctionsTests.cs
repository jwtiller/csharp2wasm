using System.Text.RegularExpressions;
using csharp2wasm;
using NUnit.Framework;

namespace unittests
{
    public class LocalFunctionTests
    {
        private readonly Csharp2WasmConverter _converter = new();

        [Test]
        public void AddTwoInts_ShouldReturnExpectedWasmCode()
        {
            var converted = _converter.Transpile("public int AddTwo(int x, int y) { return x+y; }");
            Assert.AreEqual(Regex.Unescape("(module (func (export \"AddTwo\") (param i32 i32) (result i32) local.get 0 local.get 1 i32.add))"),converted);
        }

        [Test]
        public void AddThreeInts_ShouldReturnExpectedWasmCode()
        {
            var converted = _converter.Transpile("public int AddThree(int x, int y, int z) { return x+y+z; }");
            Assert.AreEqual(Regex.Unescape("(module (func (export \"AddThree\") (param i32 i32 i32) (result i32) local.get 0 local.get 1 local.get 2 i32.add i32.add))"), converted);
        }

        [Test]
        public void SubtractTwoInts_ShouldReturnExpectedWasmCode()
        {
            var converted = _converter.Transpile("public int Subtract(int x, int y) { return x-y; }");
            Assert.AreEqual(Regex.Unescape("(module (func (export \"Subtract\") (param i32 i32) (result i32) local.get 0 local.get 1 i32.sub))"), converted);
        }

        [Test]
        public void SubtractThreeInts_ShouldReturnExpectedWasmCode()
        {
            var converted = _converter.Transpile("public int Subtract(int x, int y, int z) { return x-y-z; }");
            Assert.AreEqual(Regex.Unescape("(module (func (export \"Subtract\") (param i32 i32 i32) (result i32) local.get 0 local.get 1 local.get 2 i32.sub i32.sub))"), converted);
        }

        [Test]
        public void MixAddSub_ShouldReturnExpectedWasmCode()
        {
            var converted = _converter.Transpile("public int Mixed(int x, int y, int z) { return x+y-z; }");
            Assert.AreEqual(Regex.Unescape("(module (func (export \"Mixed\") (param i32 i32 i32) (result i32) local.get 0 local.get 1 local.get 2 i32.add i32.sub))"), converted);
        }

        [Test]
        public void MixSubAdd_ShouldReturnExpectedWasmCode()
        {
            var converted = _converter.Transpile("public int Mixed(int x, int y, int z) { return x-y+z; }");
            Assert.AreEqual(Regex.Unescape("(module (func (export \"Mixed\") (param i32 i32 i32) (result i32) local.get 0 local.get 1 local.get 2 i32.sub i32.add))"), converted);
        }

        [Test]
        public void Multiply_ShouldReturnExpectedWasmCode()
        {
            var converted = _converter.Transpile("public int Multiply(int x, int y) { return x*y; }");
            Assert.AreEqual(Regex.Unescape("(module (func (export \"Multiply\") (param i32 i32) (result i32) local.get 0 local.get 1 i32.mul))"), converted);
        }
    }
}