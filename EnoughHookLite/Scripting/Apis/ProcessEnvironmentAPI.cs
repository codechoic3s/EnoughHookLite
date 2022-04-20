using EnoughHookLite.Modules;
using EnoughHookLite.Scripting.Apis.APIWraps;
using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Apis
{
    public sealed class ProcessEnvironmentAPI : SharedAPI
    {
        private SubAPI SubAPI;
        public ProcessEnvironmentAPI(SubAPI subapi)
        {
            SubAPI = subapi;
        }

        public override void OnSetupAPI(ISharedHandler local)
        {
            local.AddDelegate("getModule", (Func<string, ManagedModule>)GetModule);

            local.AddDelegate("getRemoteMemory", (Func<ScriptRemoteMemory>)(() => { return new ScriptRemoteMemory(SubAPI.Process.RemoteMemory); }));
        }

        private ManagedModule GetModule(string name)
        {
            SubAPI.TryGetModule(name, out var module);
            return module;
        }
    }
}
