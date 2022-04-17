using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public delegate ScriptEvent ScriptDelegate(string name, string del);

    public sealed class ScriptEvent
    {
        internal (string, Script) LinkedDelegate;
        private ScriptAPI Api;

        public ScriptEvent(ScriptAPI api, (string, Script) del)
        {
            Api = api;
            LinkedDelegate = del;
        }

        public void Unlink()
        {
            Api.RemoveEvent(this);
        }
    }
}
