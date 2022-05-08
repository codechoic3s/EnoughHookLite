using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteDebugClient
{
    public sealed class RemoteLogger
    {
        private Client Cl;
        public RemoteLogger(Client cl)
        {
            Cl = cl;
        }
        public void SendLog(string log)
        {
            if (Cl.IsConnected)
                Cl.SendLog(log);
        }
    }
}
