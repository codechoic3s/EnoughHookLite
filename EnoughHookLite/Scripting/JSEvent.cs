using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public delegate JSEvent ScriptEvent(string name, string del);

    public sealed class JSEvent
    {
        internal (string, Script) LinkedDelegate;
        private JSApi Api;

        public JSEvent(JSApi api, (string, Script) del)
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
