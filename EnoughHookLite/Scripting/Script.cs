using Jint;
using Jint.Runtime.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public sealed class Script
    {
        public string Name { get; internal set; }
        public string RawScript { get; internal set; }

        private Thread ExecuteThread;

        public Engine JSEngine { get; private set; }
        internal JSLoader Loader;
        internal JSConfig Config;
        internal JSLocal Local;

        public Script(JSLoader loader, string name, string script)
        {
            Name = name;
            RawScript = script;
            Loader = loader;
            Config = new JSConfig();
            Local = new JSLocal(this, Loader.JSApi);
        }

        public void Setup()
        {
            JSEngine = new Engine();
            Local.SetupDefaultAPI();
            Local.LoadAPI();
        }

        public void Start()
        {
            ExecuteThread = new Thread(Execution);
            ExecuteThread.Start();
        }

        internal void SyncConfig()
        {
            Loader.App.ConfigManager.SyncWithCurrentScript(Name, this);
        }

        private void Execution()
        {
            LogIt("Started.");
            try
            {
                JSEngine.Execute(RawScript);
            }
            catch (Exception ex)
            {
                var cfg = Loader.App.ConfigManager.Current.Config;
                if (cfg.ScriptFullDebug)
                {
                    LogIt($"Exception on execution: {ex}");
                }
                else
                {
                    LogIt($"Exception on execution: {ex.Message}");
                }
                if (cfg.ScriptAutoReload)
                {
                    Start();
                }
            }
        }

        internal JSEvent OnNewEvent(string name, string del_name)
        {
            bool state;

            state = Loader.JSApi.Callbacks.TryGetValue(name, out List<(string, Script)> actions);
            if (state)
            {
                actions.Add((del_name, this));
                return new JSEvent(Loader.JSApi, (del_name, this));
            }
            state = Loader.JSApi.CustomCallbacks.TryGetValue(name, out actions);
            if (state)
            {
                actions.Add((del_name, this));
                return new JSEvent(Loader.JSApi, (del_name, this));
            }

            return null;
        }

        internal void LogIt(string log)
        {
            App.Log.LogIt($"({Name}) {log}");
        }
    }
}
