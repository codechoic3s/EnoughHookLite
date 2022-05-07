using EnoughHookLite.Logging;
using EnoughHookLite.Utilities.Conf;
using Jint;
using Jint.Parser;
using Jint.Runtime;
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
        internal ScriptLoader Loader;
        internal ScriptConfig Config;
        internal ScriptLocal Local;

        internal LogEntry LogScript;

        public Script(ScriptLoader loader, string name, string script)
        {
            Name = name;
            RawScript = script;
            Loader = loader;
            Config = new ScriptConfig();
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

        private void Execution()
        {
            LogScript.Log("Started.");
            try
            {
                JSEngine.Execute(RawScript);
            }
            catch (Exception ex)
            {
                var cfg = Loader.App.ConfigManager.Debug.Config;
                if (cfg.ScriptFullDebug)
                {
                    LogScript.Log($"Exception on execution: {ex}");
                }
                else
                {
                    LogScript.Log($"Exception on execution: {ex.Message}");
                }
                HandleException(cfg);
            }
        }

        private void HandleException(DebugConfig debug)
        {
            if (debug.ScriptAutoReload)
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
