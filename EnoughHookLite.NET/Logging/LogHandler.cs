using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Logging
{
    public sealed class LogHandler
    {
        private Dictionary<string, LogEntry> logEntries;
        public Action<string> Writer;
        public Action<string> AltWriter;

        public LogHandler()
        {
            logEntries = new Dictionary<string, LogEntry>();
        }

        private void OnLog(string log)
        {
            Writer?.Invoke(log);
            AltWriter?.Invoke(log);
        }
        public bool AddEntry(string name, LogEntry entry)
        {
            if (logEntries.ContainsKey(name))
                return false;
            entry.OnLog = OnLog;
            logEntries.Add(name, entry);
            return true;
        }
        public bool AddEntry(string name, LogEntry entry, Action<string> writer)
        {
            if (logEntries.ContainsKey(name))
                return false;
            entry.OnLog = (string log) => { writer(log); OnLog(log); };
            logEntries.Add(name, entry);
            return true;
        }
        public bool RemoveEntry(string name)
        {
            if (!logEntries.ContainsKey(name))
                return false;

            logEntries.Remove(name);
            return true;
        }
        public string GetAll()
        {
            string str = "";
            foreach (LogEntry entry in logEntries.Values)
            {
                str += entry.GetAll();
            }
            return str;
        }
        public (double, string[])[] GetAllEntries()
        {
            var sdict = new SortedDictionary<double, List<string>>();

            foreach (LogEntry entry in logEntries.Values)
            {
                var entrs = entry.GetAllEntries();
                var eco = entrs.LongLength;

                for (long i = 0; i < eco; i++)
                {
                    var e = entrs[i];
                    if (sdict.TryGetValue(e.Item1, out List<string> lst))
                    {
                        lst.AddRange(e.Item2);
                    }
                    else
                        sdict.Add(e.Item1, new List<string>(e.Item2));
                }
            }

            var entrss = new (double, string[])[sdict.Count];

            var o = 0;
            foreach(var ee in sdict)
            {
                entrss[o] = (ee.Key, ee.Value.ToArray());
                o++;
            }

            return entrss;
        }

        public string GetAllEntriesAsString()
        {
            var entrs = GetAllEntries();
            string str = "";

            var eco = entrs.LongLength;
            for (long i = 0; i < eco; i++)
            {
                var i2 = entrs[i].Item2;
                var i2co = i2.LongLength;
                for (long o = 0; o < i2co; o++)
                {
                    str += i2[o] + '\n';
                }
            }

            return str;
        }
    }
}
