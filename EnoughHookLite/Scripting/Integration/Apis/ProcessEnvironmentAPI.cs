using EnoughHookLite.Modules;
using EnoughHookLite.Scripting.Integration.Apis.APIWraps;
using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration.Apis
{
    public sealed class ProcessEnvironmentAPI : SharedAPI
    {
        private SubAPI SubAPI;
        public ProcessEnvironmentAPI(SubAPI subapi)
        {
            SubAPI = subapi;
        }

        protected override void OnSetupModule(ScriptModule module)
        {
            module.AddDelegate("getModule", (Func<string, ManagedModule>)GetModule);

            module.AddDelegate("getRemoteMemory", (Func<ScriptRemoteMemory>)(() => { return new ScriptRemoteMemory(SubAPI.Process.RemoteMemory); }));
        }

        protected override void OnSetupTypes(ISharedGlobalHandler handler)
        {
            
        }

        private ManagedModule GetModule(string name)
        {
            SubAPI.TryGetModule(name, out var module);
            return module;
        }
    }
}
