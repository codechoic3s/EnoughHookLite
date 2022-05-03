using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Logging
{
    public sealed class LogHandler
    {
        private List<LogEntry> logEntries;

        public LogHandler()
        {
            logEntries = new List<LogEntry>();
        }
    }
}
