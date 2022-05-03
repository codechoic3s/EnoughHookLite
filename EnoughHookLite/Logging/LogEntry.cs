using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Logging
{
    public sealed class LogEntry
    {
        private ConcurrentQueue<string> _log;
        internal Action<string> OnLog;

        public LogEntry()
        {
            _log = new ConcurrentQueue<string>();
        }

        public void Log(string log)
        {
            _log.Enqueue(log);
            OnLog(log);
        }
        public void Log(object logobj)
        {
            var log = logobj.ToString();
            OnLog(log);
            _log.Enqueue(log);
        }
    }
}
