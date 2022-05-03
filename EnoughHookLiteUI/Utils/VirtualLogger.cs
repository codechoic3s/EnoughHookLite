using EnoughHookLiteUI.ScriptAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.Utils
{
    public sealed class VirtualLogger
    {
        private List<string> log;
        public VirtualLoggerAPI VirtualLoggerAPI { get; private set; }
        public VirtualLogger()
        {
            log = new List<string>();
            VirtualLoggerAPI = new VirtualLoggerAPI();
        }

        public void Log(string logg)
        {
            log.Add(logg);
        }
    }
}
