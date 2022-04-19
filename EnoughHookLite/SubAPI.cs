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
        public SubAPI(AModules am)
        {
            PointManager = new PointManager(this);
            TypesParser = new TypesParser(PointManager);
            ParseModules(am);
        }

        public bool ParseDefaultModules()
        {
            LogIt("Parsing Engine...");
            if (!TypesParser.TryParse(Engine, true))
            {
                LogIt("Failed parse Engine.");
                return false;
            }
            LogIt("Parsed Engine.");
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
                    LogIt($"Not founded custom module {AModules.ClientModule}");
                    continue;
                }

                CustomModules.Add(modulename, new ManagedModule(custommodule));
            }
        }

        public void ProcessFetch(string processname)
        {
            LogIt($@"Waiting process ""{processname}""...");
            while (Process is null)
            {
                Process = Process.FindProcess(processname);
                Thread.Sleep(1000);
            }
            LogIt("Process finded!");
            Process.AllocateHandles();
        }
        public bool ModulesFetch()
        {
            Module clientm = Process.GetModule(AModules.ClientModule, out bool cf);
            Module enginem = Process.GetModule(AModules.EngineModule, out bool ef);

            if (!cf)
            {
                LogIt($"Not founded client as {'"'}{AModules.ClientModule}{'"'}");
                return false;
            }
            else if (!ef)
            {
                LogIt($"Not founded engine as {'"'}{AModules.EngineModule}{'"'}");
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

        private void LogIt(string log)
        {
            App.Log.LogIt("[SubAPI] " + log);
        }
    }
}
