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
                var lpath = path + "/" + ClientClassesDumpCPP;
                LogDebugTools?.Log("Dumping ClientClasses as classes for C++ to " + lpath + "...");
                App.SubAPI.PointManager.ClientClassParser.DumpCppClasses(lpath);
                LogDebugTools?.Log("Dumped!");
            }
        }
        public void OnStartDebug()
        {
            if (ConfigManager.Debug.Config.RemoteDebug)
            {
                RemoteDebugger = new RemoteDebugger();
                RemoteDebugger.Start(ConfigManager.Debug.Config);
                App.LogHandler.AltWriter = RemoteDebugger.Logger.SendLog;
                LogDebugTools.Log("RemoteDebugger installed.");
            }
        }

        private void ActiveConnect()
        {

        }

        private void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
