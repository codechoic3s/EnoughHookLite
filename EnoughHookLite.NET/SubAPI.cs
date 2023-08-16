using EnoughHookLite.Logging;
using EnoughHookLite.Modules;
using EnoughHookLite.Pointing;
using EnoughHookLite.Sys;
using EnoughHookLite.Utilities.Conf;
using System.Collections.Generic;
using System.Threading;

namespace EnoughHookLite
{
    public sealed class SubAPI
    {
        public Process Process { get; private set; }
        public Client Client { get; private set; }
        public Engine Engine { get; private set; }
        private Dictionary<string, ManagedModule> CustomModules;

        public PointManager PointManager { get; private set; }
        public TypesParser TypesParser { get; private set; }

        public AModules AModules { get; private set; }

        private LogEntry LogSubAPI;
        public SubAPI(AModules am)
        {
            PointManager = new PointManager(this);
            TypesParser = new TypesParser(PointManager);
            ParseModules(am);

            LogSubAPI = new LogEntry(() => { return $"[SubAPI] "; });
            App.LogHandler.AddEntry($"SubAPI", LogSubAPI);
        }

        public bool ParseDefaultModules()
        {
            if (!TypesParser.TryParse(Engine, true))
            {
                LogSubAPI.Log("Failed parse Engine.");
                return false;
            }
            return true;
        }

        public bool TryGetModule(string name, out ManagedModule module)
        {
            if (name == AModules.ClientModule)
            {
                module = Client;
                return true;
            }
            else if (name == AModules.EngineModule)
            {
                module = Engine;
                return true;
            }
            return CustomModules.TryGetValue(name, out module);
        }

        private void ParseModules(AModules am)
        {
            AModules = am;
            CustomModules = new Dictionary<string, ManagedModule>();
            var cmco = AModules.CustomModules.Count;
            for (var i = 0; i < cmco; i++)
            {
                var modulename = AModules.CustomModules[i];

                Module custommodule = Process.GetModule(modulename, out bool cmb);
                if (!cmb)
                {
                    LogSubAPI.Log($"Not founded custom module {AModules.ClientModule}");
                    continue;
                }

                CustomModules.Add(modulename, new ManagedModule(custommodule));
            }
        }

        public void ProcessFetch(string processname)
        {
            LogSubAPI.Log($@"Waiting process ""{processname}""...");
            while (Process is null)
            {
                Process = Process.FindProcess(processname);
                Thread.Sleep(1000);
            }
            LogSubAPI.Log("Process finded!");
            Process.AllocateHandles();
        }
        public bool ModulesFetch()
        {
            Module clientm = Process.GetModule(AModules.ClientModule, out bool cf);
            Module enginem = Process.GetModule(AModules.EngineModule, out bool ef);

            if (!cf)
            {
                LogSubAPI.Log($"Not founded client as {'"'}{AModules.ClientModule}{'"'}");
                return false;
            }
            else if (!ef)
            {
                LogSubAPI.Log($"Not founded engine as {'"'}{AModules.EngineModule}{'"'}");
                return false;
            }
            Client = new Client(clientm, this);
            Engine = new Engine(enginem);
            return true;
        }

        public void StartAll()
        {
            Client.Start();
        }
    }
}
