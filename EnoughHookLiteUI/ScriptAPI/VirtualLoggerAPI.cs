using EnoughHookLite.Scripting;
using EnoughHookLiteUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.ScriptAPI
{
    public sealed class VirtualLoggerAPI : SharedAPI
    {
        private VirtualLogger VirtualLogger;
        public VirtualLoggerAPI(VirtualLogger vl)
        {
            VirtualLogger = vl;
        }
        public override void OnSetupAPI(ISharedHandler handler)
        {
            handler.AddDelegate("getLog", VirtualLogger.);
        }
    }
}
