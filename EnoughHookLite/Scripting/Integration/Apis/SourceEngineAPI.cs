using EnoughHookLite.GameClasses;
using EnoughHookLite.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration.Apis
{
    public sealed class SourceEngineAPI : SharedAPI
    {
        private Client Client;
        private Engine Engine;

        public SourceEngineAPI(Client cl, Engine eng)
        {
            Client = cl;
            Engine = eng;
        }

        protected override void OnSetupModule(ScriptModule module)
        {
            module.AddDelegate("getEntityList", (Func<EntityList>)(() => Client.EntityList));
        }

        protected override void OnSetupTypes(ISharedGlobalHandler handler)
        {
            
        }
    }
}
