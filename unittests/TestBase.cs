using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasmtime;

namespace unittests
{
    public class TestBase<T>
    {
        public string WebassemblyText { get; set; }

        public T ExecuteWasmCode(string moduleName, string function, params object[] parameters)
        {
            using var engine = new Engine();
            using (var module = Module.FromText(engine, moduleName, WebassemblyText))
            {
                using var linker = new Linker(engine);
                using var store = new Store(engine);
                var instance = linker.Instantiate(store, module);
                var run = instance.GetFunction(store, function);
                var result = run?.Invoke(store, parameters);
                return (T)result;
            }
        }
    }
}
