using EnoughHookLite.Logging;
using EnoughHookLite.Utilities.Conf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public sealed class DebugTools
    {
        private const string DebugFolder = "debug";
        private const string ClientClassesDumpCPP = "clientclasses.cpp";
        public App App { get; private set; }
        public ConfigManager ConfigManager { get; private set; }
        public RemoteDebugger RemoteDebugger { get; private set; }

        private LogEntry LogDebugTools;
        public DebugTools(App app, ConfigManager cfgs)
        {
            App = app;
            ConfigManager = cfgs;

            LogDebugTools = new LogEntry(() => { return $"[DebugTools] "; });
            App.LogHandler.AddEntry($"DebugTools", LogDebugTools);
        }
        public void OnDumpDebug()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "/" + DebugFolder;
            if (ConfigManager.Debug.Config.DumpClientClassesCPP)
            {
                CreateFolder(path);
                App.SubAPI.PointManager.ClientClassParser.DumpCppClasses(path + "/" + ClientClassesDumpCPP);
            }
        }
        public bool OnStartDebug()
        {
            if (ConfigManager.Debug.Config.RemoteDebug)
            {
                RemoteDebugger = new RemoteDebugger();
                RemoteDebugger.Start(ConfigManager.Debug.Config);
                while (!RemoteDebugger.IsConnected)
                {
                    if (RemoteDebugger.HasError)
                    {
                        LogDebugTools.Log("Failed connect remote debug.");
                        return false;
                    }
                }
                App.LogHandler.AltWriter = RemoteDebugger.Logger.SendLog;
                LogDebugTools.Log("Successfully connected to remotedebugger host.");
            }
            return true;
        }

        private void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
