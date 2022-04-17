using EnoughHookLite.Modules;
using EnoughHookLite.Pointing;
using EnoughHookLite.Sys;
using EnoughHookLite.Utilities.Conf;
using System.Threading;

namespace EnoughHookLite
{
    public sealed class SubAPI
    {
        public Process Process { get; private set; }
        public Client Client { get; private set; }
        public Engine Engine { get; private set; }
        public PointManager PointManager { get; private set; }

        private AModules Amodules;
        public SubAPI(AModules am)
        {
            PointManager = new PointManager(this);
            Amodules = am;
        }

        public void ProcessFetch()
        {
            LogIt("Waiting process...");
            while (Process is null)
            {
                Process = Process.FindProcess("csgo");
                Thread.Sleep(1000);
            }
            Process.AllocateHandles();
            LogIt("Process finded!");
        }
        public bool ModulesFetch()
        {
            Module clientm = Process.GetModule(Amodules.ClientModule, out bool cf);
            Module enginem = Process.GetModule(Amodules.EngineModule, out bool ef);

            if (!cf)
            {
                LogIt($"Not founded client as {'"'}{Amodules.ClientModule}{'"'}");
                return false;
            }
            else if (!ef)
            {
                LogIt($"Not founded engine as {'"'}{Amodules.EngineModule}{'"'}");
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
