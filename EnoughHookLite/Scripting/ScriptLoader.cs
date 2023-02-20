using EnoughHookLite.Logging;
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
        private Dictionary<string, Script> Scripts;
        private string Path;

        public ScriptAPI ScriptApi { get; private set; }

        internal ScriptHost ScriptHost;

        private LogEntry LogScriptLoader;

        public ScriptLoader(ScriptHost host)
        {
            Scripts = new Dictionary<string, Script>();
            Path = AppDomain.CurrentDomain.BaseDirectory + "\\scripts";
            ScriptHost = host;
            ScriptApi = new ScriptAPI(ScriptHost);

            LogScriptLoader = new LogEntry(() => { return "[ScriptLoader] "; });
            App.LogHandler.AddEntry("ScriptLoader", LogScriptLoader);
        }

        public void SetupGlobalAPI()
        {
            ScriptApi.Setup();
            ScriptApi.LoadGlobalAPI();
        }
        public void SetupScripts()
        {
            foreach (var item in Scripts)
            {
                var script = item.Value;
                script.Setup();
            }
        }

        public void AllocateScripts()
        {
            LogScriptLoader.Log($"Allocating scripts...");
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            string[] filteredFiles = Directory // gettings files
            .GetFiles(Path, "*.*")
            .Where(file => file.ToLower().EndsWith("py"))
            .ToArray();

            long fco = filteredFiles.LongLength;
            for (long i = 0; i < fco; i++)
            {
                var fpath = filteredFiles[i];
                if (File.Exists(fpath) && !Scripts.ContainsKey(fpath))
                {
                    var splited1 = fpath.Split('\\');
                    var splited2 = splited1[splited1.LongLength - 1].Split('.');
                    var sp1 = new List<string>(splited1);
                    sp1.RemoveAt(sp1.Count - 1);
                    var npath = "";
                    for (var o = 0; o < sp1.Count; o++)
                    {
                        npath += sp1[o];
                        if (o != sp1.Count - 1)
                            npath += "\\";
                    }

                    var fname = splited2[0];

                    var rawscript = File.ReadAllText(fpath); // reading file

                    var sc = new Script(ScriptHost, fname, npath, rawscript); // creating script
                    Scripts.Add(fpath, sc);
                }
            }
            LogScriptLoader.Log($"Allocated {Scripts.Count} scripts.");
        }

        public void StartAll()
        {
            foreach (var item in Scripts)
            {
                var script = item.Value;
                script.Start();
            }    
        }
    }
}
