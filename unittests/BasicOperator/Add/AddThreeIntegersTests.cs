﻿using System.Text.RegularExpressions;
using csharp2wasm;
using NUnit.Framework;
using Wasmtime;
using Module = Wasmtime.Module;
using ValueType = csharp2wasm.ValueType;

namespace unittests.BasicOperator.Add
{
    [TestFixture]
    public class AddThreeIntegersTests
    {
        private string wasmTextCode;

        [SetUp]
        public void Setup()
        {
            var module = new csharp2wasm.Module();
            var parameters = new[] { csharp2wasm.ValueType.Int32, ValueType.Int32, ValueType.Int32 };
            var signature = module.AddFunctionType("add", ValueType.Int32, parameters);


            var x = module.GetLocal(0, ValueType.Int32);
            var y = module.GetLocal(1, ValueType.Int32);
            var z = module.GetLocal(2, ValueType.Int32);
            var add = module.Binary(BinaryOperator.AddInt32, x, y, z);


            module.AddFunction("mathFunc", signature, add);
            wasmTextCode = module.ToWat();
        }

        [Test]
        public void AssertExpectedWebassemblyText()
        {
            Assert.AreEqual(
                Regex.Unescape(
                    $"(module (type $add (func (param i32 i32 i32) (result i32))) (export \"mathFunc\" (func $module/mathFunc)) (func $module/mathFunc (type $add) (param $0 i32) (param $1 i32) (param $2 i32) (result i32) (i32.add (get_local $0) (get_local $1) i32.add (get_local $2))))"),
                wasmTextCode);
        }

        [Test]
        public void ShouldReturn_Nine()
        {
            using var engine = new Engine();
            using (var module = Module.FromText(engine, "add", wasmTextCode))
            {
                using var linker = new Linker(engine);
                using var store = new Store(engine);
                var instance = linker.Instantiate(store, module);
                var run = instance.GetFunction(store, "mathFunc");
                var result = run?.Invoke(store, 3, 3, 3);
                Assert.AreEqual(9, result);
            }
        }
    }
}
