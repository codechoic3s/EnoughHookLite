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
        public bool HasError { get; private set; }
        public void Start(DebugConfig cfg)
        {
            Client = new Client
            {
                ConnectionOptions = new ConnectionOptions()
                {
                    Host = cfg.RemoteDebugHost,
                    Port = cfg.RemoteDebugPort
                },
                FailedConnect = FailedConnect,
                ConnectionError = ConnectionError
            };
            Logger = new RemoteLogger(Client);
            Task.Run(Client.Start);
        }

        private void FailedConnect()
        {
            HasError = true;
        }
        private void ConnectionError()
        {
            HasError = true;
            Console.WriteLine("Connection error.");
        }
    }
}
