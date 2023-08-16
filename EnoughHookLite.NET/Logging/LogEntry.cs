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
        private ConcurrentQueue<(double, string)> _log;
        internal Action<string> OnLog;
        public Func<string> Writer;

        public LogEntry(Func<string> writer)
        {
            _log = new ConcurrentQueue<(double, string)>();
            Writer = writer;
        }

        public void Log(string log)
        {
            var lg = Writer() + log;
            _log.Enqueue((DateTime.Now.TimeOfDay.TotalMilliseconds, lg));
            OnLog?.Invoke(lg);
        }
        public void Log(object logobj)
        {
            var log = logobj.ToString();
            Log(log);
        }
        public string GetAll()
        {
            string str = "";
            foreach (var log in _log)
            {
                str += log.Item2 + '\n';
            }
            return str;
        }
        public (double, string[])[] GetAllEntries()
        {
            var ar = _log.ToArray();
            var ndict = new Dictionary<double, List<string>>();

            var aco = ar.LongLength;
            for (long i = 0; i < aco; i++)
            {
                var a = ar[i];
                if (ndict.TryGetValue(a.Item1, out List<string> lst))
                    lst.Add(a.Item2);
                else
                    ndict.Add(a.Item1, new List<string>() { a.Item2 });
            }

            var nar = new (double, string[])[ndict.Count];
            ulong o = 0;
            foreach (var n in ndict)
            {
                nar[o] = (n.Key, n.Value.ToArray());
                o++;
            }

            return nar;
        }
    }
}
