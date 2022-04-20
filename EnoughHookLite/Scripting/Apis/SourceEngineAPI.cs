using EnoughHookLite.GameClasses;
using EnoughHookLite.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Apis
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

        public override void OnSetupAPI(ISharedHandler local)
        {
            local.AddDelegate("getEntityList", (Func<EntityList>)(() => Client.EntityList));
        }
    }
}
