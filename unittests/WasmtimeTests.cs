using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Wasmtime;

namespace unittests
{
    [TestFixture]
    public class WasmtimeTests
    {
        [Test]
        public void AddThreeAndThree_ShouldReturnSix()
        {
            using var engine = new Engine();
            using (var module = Module.FromText(engine, "add", "(module (type $add (func (param i32 i32) (result i32)))  (export \"add\" (func $module/add)) (func $module/add (type $add) (param $0 i32) (param $1 i32) (result i32) (i32.add (get_local $0) (get_local $1))))"))
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
