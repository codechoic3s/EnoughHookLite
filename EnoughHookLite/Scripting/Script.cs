using EnoughHookLite.Logging;
using EnoughHookLite.Utilities;
using EnoughHookLite.Utilities.Conf;
using Jint;
using Jint.Native;
using Jint.Parser;
using Jint.Runtime;
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
    public sealed class Script
    {
        public string Name { get; internal set; }
        public string RawScript { get; internal set; }
        public string Path { get; internal set; }

        private Thread ExecuteThread;

        public Engine JSEngine { get; private set; }
        internal ScriptLoader Loader;
        internal ScriptConfig Config;
        internal ConfigAPI ConfigAPI;
        internal ScriptLocal Local;

        internal LogEntry LogScript { get; private set; }

        public Script(ScriptLoader loader, string name, string path, string script)
        {
            Name = name;
            Path = path;
            RawScript = script;
            Loader = loader;
            Config = new ScriptConfig();
            ConfigAPI = new ConfigAPI();
            Local = new ScriptLocal(loader.App.SubAPI, this, Loader.JSApi);

            LogScript = new LogEntry(() => { return $"({Name}) "; });
            App.LogHandler.AddEntry($"Script:{Name}", LogScript);
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

        internal JsValue LoadScript(string path)
        {
            var cpath = Path + path;
            if (!File.Exists(cpath))
            {
                LogScript.Log($"Failed load script on {cpath}");
                return null;
            }
            return JSEngine.Execute(File.ReadAllText(cpath)).GetCompletionValue();
        }

        private void Execution()
        {
            LogScript.Log("Started.");
            try
            {
                JSEngine.Execute(RawScript);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                HandleAutoReload();
            }
        }

        public void HandleException(Exception ex)
        {
            var cfg = Loader.App.ConfigManager.Debug.Config;
            LogScript.Log($"Exception on execution: {ExceptionHandler.HandleEception(ex, cfg.ScriptFullDebug)}");
        }

        private void HandleAutoReload()
        {
            var cfg = Loader.App.ConfigManager.Debug.Config;
            if (cfg.ScriptAutoReload)
            {
                Start();
            }
        }
        private bool HasAction(List<(string, Script)> actions, string act_name)
        {
            var aco = actions.Count;
            for (var i = 0; i < aco; i++)
            {
                var action = actions[i];
                if (action.Item1 == act_name)
                    return true;
            }
            return false;
        }
        private void HasActionRemove(List<(string, Script)> actions, string act_name)
        {
            var aco = actions.Count;
            for (var i = 0; i < aco; i++)
            {
                var action = actions[i];
                if (action.Item1 == act_name)
                {
                    actions.RemoveAt(i);
                    return;
                }
            }
        }
        internal void OnRemEvent(string name, string del_name)
        {
            bool state;

            state = Loader.JSApi.Callbacks.TryGetValue(name, out List<(string, Script)> actions);
            if (state)
            {
                HasActionRemove(actions, del_name);
                return;
            }
            state = Loader.JSApi.CustomCallbacks.TryGetValue(name, out actions);
            if (state)
            {
                HasActionRemove(actions, del_name);
                return;
            }
        }
        internal ScriptEvent OnNewEvent(string name, string del_name)
        {
            bool state;

            state = Loader.JSApi.Callbacks.TryGetValue(name, out List<(string, Script)> actions);
            if (state)
            {
                actions.Add((del_name, this));
                return new ScriptEvent(Loader.JSApi, (del_name, this));
            }
            state = Loader.JSApi.CustomCallbacks.TryGetValue(name, out actions);
            if (state)
            {
                actions.Add((del_name, this));
                return new ScriptEvent(Loader.JSApi, (del_name, this));
            }

            return null;
        }
    }
}
