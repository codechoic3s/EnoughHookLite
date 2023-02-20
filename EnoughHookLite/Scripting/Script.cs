using EnoughHookLite.Logging;
using EnoughHookLite.Scripting.Integration;
using EnoughHookLite.Utilities;
using EnoughHookLite.Utilities.Conf;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
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

        internal Thread ExecuteThread;

        public ScriptScope ScriptScope;

        internal ScriptHost ScriptHost;
        internal ScriptLoader Loader;
        internal ScriptConfig Config;
        internal ConfigAPI ConfigAPI;
        internal ScriptLocal Local;

        internal LogEntry LogScript { get; private set; }

        public Script(ScriptHost host, string name, string path, string script)
        {
            ScriptHost = host;
            Loader = ScriptHost.ScriptLoader;
            Name = name;
            Path = path;
            RawScript = script;

            Config = new ScriptConfig();
            ConfigAPI = new ConfigAPI();
            Local = new ScriptLocal(this, Loader.ScriptApi);

            LogScript = new LogEntry(() => { return $"({Name}) "; });
            App.LogHandler.AddEntry($"Script:{Name}", LogScript);
        }

        public void Setup()
        {
            ScriptScope = ScriptHost.ScriptEngine.CreateScope();
            //ScriptScope = ScriptHost.ScriptEngine.CreateScope();

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
            Loader.ScriptHost.App.ConfigManager.SyncWithCurrentScript(Name, this);
        }

        private void Execution()
        {
            LogScript.Log("Started.");
            try
            {
                ScriptHost.ExecuteDyn(this);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                HandleAutoReload();
            }
        }

        public void HandleException(Exception ex)
        {
            var cfg = Loader.ScriptHost.App.ConfigManager.Debug.Config;
            LogScript.Log($"Exception on execution: {ExceptionHandler.HandleEception(ex, cfg.ScriptFullDebug)}");
        }

        private void HandleAutoReload()
        {
            var cfg = Loader.ScriptHost.App.ConfigManager.Debug.Config;
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

            state = Loader.ScriptApi.Callbacks.TryGetValue(name, out List<(string, Script)> actions);
            if (state)
            {
                HasActionRemove(actions, del_name);
                return;
            }
            state = Loader.ScriptApi.CustomCallbacks.TryGetValue(name, out actions);
            if (state)
            {
                HasActionRemove(actions, del_name);
                return;
            }
        }
        internal ScriptEvent OnNewEvent(string name, string del_name)
        {
            bool state;

            state = Loader.ScriptApi.Callbacks.TryGetValue(name, out List<(string, Script)> actions);
            if (state)
            {
                actions.Add((del_name, this));
                return new ScriptEvent(Loader.ScriptApi, (del_name, this));
            }
            state = Loader.ScriptApi.CustomCallbacks.TryGetValue(name, out actions);
            if (state)
            {
                actions.Add((del_name, this));
                return new ScriptEvent(Loader.ScriptApi, (del_name, this));
            }

            return null;
        }
    }
}
