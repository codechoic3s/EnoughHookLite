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
        public Func<string> Writer;

        public LogEntry(Func<string> writer)
        {
            _log = new ConcurrentQueue<string>();
            Writer = writer;
        }

        public void Log(string log)
        {
            var lg = Writer() + log;
            _log.Enqueue(lg);
            OnLog?.Invoke(lg);
        }
        public void Log(object logobj)
        {
            var log = logobj.ToString();
            var lg = Writer() + log;
            _log.Enqueue(log);
            OnLog?.Invoke(log);
        }
        public string GetAll()
        {
            string str = "";
            foreach (var log in _log)
            {
                str += log + '\n';
            }
            return str;
        }
    }
}
