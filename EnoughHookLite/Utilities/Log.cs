using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public sealed class Log
    {
        private List<string> Logs;
        public Action<string> LogAction;

        public Log()
        {
            Logs = new List<string>();
        }

        public void LogIt(string l, bool withaction = true)
        {
            Logs.Add(l);
            if (withaction)
                LogAction(l);
        }

        public string Get()
        {
            string str = "";
            int lco = Logs.Count;
            for (int i = 0; i < lco; i++)
            {
                str += $"{Logs[i]}\n";
            }
            return str;
        }
    }
}
