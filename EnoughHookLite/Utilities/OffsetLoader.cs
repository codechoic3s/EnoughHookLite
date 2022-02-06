using EnoughHookLite.Offsets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public sealed class OffsetLoader
    {
        public string Path;
        public csgo Offsets { get; private set; }

        public OffsetLoader()
        {
            Path = AppDomain.CurrentDomain.BaseDirectory + @"/offsets.json";
        }

        public void Load()
        {
            if (!File.Exists(Path))
            {
                LogIt("Failed locate offset file!!!");
                return;
            }

            Offsets = JsonConvert.DeserializeObject<csgo>(File.ReadAllText(Path));

            LogIt("Loaded offsets.");
        }

        private void LogIt(string log)
        {
            App.Log.LogIt($"[OffsetLoader] " + log);
        }
    }
}
