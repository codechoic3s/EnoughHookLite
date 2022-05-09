using EnoughHookLite.Logging;
using EnoughHookLite.Utilities.Conf;
using RemoteDebugClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public sealed class RemoteDebugger
    {
        public Client Client { get; private set; }
        public RemoteLogger Logger { get; private set; }
        public bool IsConnected => Client.IsConnected;

        private LogEntry LogRemoteDebug;

        public RemoteDebugger()
        {
            LogRemoteDebug = new LogEntry(() => { return "[RemoteDebugger] "; });
            App.LogHandler.AddEntry("RemoteDebugger", LogRemoteDebug);
        }
        public void Start(DebugConfig cfg)
        {
            Client = new Client
            {
                ConnectionOptions = new ConnectionOptions()
                {
                    Host = cfg.RemoteDebugHost,
                    Port = cfg.RemoteDebugPort
                },
                ConnectionError = ConnectionError,
                Connected = Connected,
            };
            Logger = new RemoteLogger(Client);
            Task.Run(Client.Start);
        }
        private void ConnectionError()
        {
            LogRemoteDebug.Log("Connection interrupted.");
        }
        private void Connected()
        {
            Logger.SendLog(App.LogHandler.GetAllEntriesAsString());
            LogRemoteDebug.Log("Successfully connected to remotedebugger host.");
        }
    }
}
