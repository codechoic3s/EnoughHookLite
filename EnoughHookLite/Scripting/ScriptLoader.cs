using Jint;
using Jint.Runtime.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public sealed class ScriptLoader
    {
        internal const string Logit = "[ScriptEngine]";
        private Dictionary<string, Script> Scripts;
        private string Path;

        public ScriptAPI JSApi { get; private set; }

        internal App App;

        public ScriptLoader(App app)
        {
            Scripts = new Dictionary<string, Script>();
            Path = AppDomain.CurrentDomain.BaseDirectory + "\\scripts";
            App = app;
            JSApi = new ScriptAPI(app);
        }

        public void SetupAll()
        {
            foreach (var item in Scripts)
            {
                var script = item.Value;
                script.Setup();
            }
        }

        public void AllocateScripts()
        {
            LogIt($"Allocating scripts...");
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            string[] filteredFiles = Directory
            .GetFiles(Path, "*.*")
            .Where(file => file.ToLower().EndsWith("js"))
            .ToArray();

            long fco = filteredFiles.LongLength;
            for (long i = 0; i < fco; i++)
            {
                var fpath = filteredFiles[i];
                if (File.Exists(fpath) && !Scripts.ContainsKey(fpath))
                {
                    var splited1 = fpath.Split('\\');
                    var splited2 = splited1[splited1.LongLength - 1].Split('.');
                    var fname = splited2[0];
                    var rawscript = File.ReadAllText(fpath);
                    var sc = new Script(this, fname, rawscript);
                    Scripts.Add(fpath, sc);
                }
            }
            LogIt($"Allocated {Scripts.Count} scripts.");
        }

        public void StartAll()
        {
            foreach (var item in Scripts)
            {
                var script = item.Value;
                script.Start();
            }    
        }

        private void LogIt(string log)
        {
            App.Log.LogIt($"{Logit} " + log);
        }
    }
}
