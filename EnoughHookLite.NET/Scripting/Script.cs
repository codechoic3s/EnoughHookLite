using EnoughHookLite.Logging;
using EnoughHookLite.Scripting.Integration;
using EnoughHookLite.Utilities;
using EnoughHookLite.Utilities.Conf;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
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

        public Microsoft.CodeAnalysis.Scripting.Script ScriptScope;

        internal ScriptHost ScriptHost;
        internal ScriptLoader Loader;
        internal ScriptConfig Config;

        internal LogEntry LogScript { get; private set; }

        public Script(ScriptHost host, string name, string path, string script)
        {
            ScriptHost = host;
            Loader = ScriptHost.ScriptLoader;
            Name = name;
            Path = path;
            RawScript = script;

            Config = new ScriptConfig();

            LogScript = new LogEntry(() => { return $"({Name}) "; });
            App.LogHandler.AddEntry($"Script:{Name}", LogScript);
        }

        public void Setup()
        {
            ScriptScope = ScriptHost.CreateScope(this); 
            //ScriptScope = ScriptHost.ScriptEngine.CreateScope();
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
                ScriptScope.RunAsync();
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
    }
}
