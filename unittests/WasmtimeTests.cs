using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp2wasm;
using NUnit.Framework;
using Wasmtime;
using Module = Wasmtime.Module;
using ValueType = csharp2wasm.ValueType;

namespace unittests
{
    [TestFixture]
    public class WasmtimeTests
    {
        private string wasmTextCode;
        [SetUp]
        public void Setup()
        {
            var module = new csharp2wasm.Module();
            var parameters = new[] { csharp2wasm.ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);


            var x = module.GetLocal(0, ValueType.Int32);
            var y = module.GetLocal(1, ValueType.Int32);
            var add = module.Binary(BinaryOperator.AddInt32, x, y);


            module.AddFunction("add", signature, add);
            wasmTextCode = module.ToWat();
        }

        [Test]
        public void AddThreeAndThree_ShouldReturnSix()
        {
            using var engine = new Engine();
            using (var module = Module.FromText(engine, "add", wasmTextCode))
            {
                using var linker = new Linker(engine);
                using var store = new Store(engine);
                var instance = linker.Instantiate(store, module);
                var run = instance.GetFunction(store, "add");
                var result = run?.Invoke(store, 3, 3);
                Assert.AreEqual(6,result);
            }
        }
    }
}
