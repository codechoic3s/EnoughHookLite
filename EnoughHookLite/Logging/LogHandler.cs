using System;
using System.Collections.Generic;
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
    }
}
