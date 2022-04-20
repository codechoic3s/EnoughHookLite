using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RemoteDebugClient
{
    public sealed class Client
    {
        private TcpClient TcpClient;
        private NetworkStream NetworkStream;

        public ConnectionOptions ConnectionOptions;
        public bool IsConnected { get; private set; }

        public Action FailedConnect;
        public Action ConnectionError;

        public void Start()
        {
            TcpClient = new TcpClient();
            if (!TryConnect())
                FailedConnect?.Invoke();
        }

        private bool TryConnect()
        {
            try
            {
                TcpClient.Connect(ConnectionOptions.Host, ConnectionOptions.Port);
                NetworkStream = TcpClient.GetStream();
                IsConnected = true;
            }
            catch
            {
                IsConnected = false;
            }
            return IsConnected;
        }

        internal void SendLog(string log)
        {
            var memstream = new MemoryStream();

            var estr = Encoding.Unicode.GetBytes(log);
            var len = estr.Length;

            var blen = BitConverter.GetBytes(len);
            //memstream.Write(blen, 0, blen.Length);
            memstream.Write(estr, 0, len);

            var mbuf = memstream.GetBuffer();
            try
            {
                NetworkStream.Write(mbuf, 0, mbuf.Length);
            }
            catch
            {
                ConnectionError?.Invoke();
            }
            memstream.Close();
        }
    }
}
