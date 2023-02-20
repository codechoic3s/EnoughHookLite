using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration
{
    public abstract class SharedAPI
    {
        public bool Types { get; private set; }
        public bool Module { get; private set; }
        public void SetupTypes(ISharedGlobalHandler handler)
        {
            if (!Types)
            {
                OnSetupTypes(handler);
                Types = true;
            }
        }
        public void SetupModule(ScriptModule module)
        {
            if (!Module)
            {
                OnSetupModule(module);
                Module = true;
            }
        }
        protected abstract void OnSetupTypes(ISharedGlobalHandler handler);
        protected abstract void OnSetupModule(ScriptModule module);
    }
}
