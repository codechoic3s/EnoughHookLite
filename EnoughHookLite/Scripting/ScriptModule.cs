using EnoughHookLite.Scripting.Integration;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public class ScriptModule : ISharedVarsHandler
    {
        public string Name { get; set; }
        public ScriptScope Module;
        public ScriptModule(ScriptEngine engine, string name) 
        {
            Name = name;
            Module = engine.CreateModule(Name);
        }
        public void AddValue(string name, object val)
        {
            Module.SetVariable(name, val);
        }
        public void AddDelegate(string name, Delegate del)
        {
            Module.SetVariable(name, del);
        }
    }
}
