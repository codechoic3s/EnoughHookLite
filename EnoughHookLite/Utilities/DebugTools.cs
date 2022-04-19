using EnoughHookLite.Utilities.Conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public sealed class DebugTools
    {
        private const string DebugFolder = "debug";
        private const string ClientClassesDumpCPP = "clientclasses.cpp";

        public SubAPI SubAPI { get; private set; }
        public ConfigManager ConfigManager { get; private set; }

        public DebugTools(SubAPI sub, ConfigManager cfgs)
        {
           SubAPI = sub;
           ConfigManager = cfgs;
        }

        public void OnStartDebug()
        {
            if (ConfigManager.Debug.Config.DumpClientClassesCPP)
                SubAPI.PointManager.ClientClassParser.DumpCppClasses(AppDomain.CurrentDomain.BaseDirectory + "/" + DebugFolder + "/" + ClientClassesDumpCPP);
        }
    }
}
