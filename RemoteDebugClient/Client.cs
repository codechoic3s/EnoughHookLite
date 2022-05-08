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

        public Action ConnectionError;
        public Action Connected;

        private Thread ConnectionThread;

        public void Start()
        {
            ConnectionThread = new Thread(Connecting);
            ConnectionThread.IsBackground = true;
            ConnectionThread.Start();
        }

        private void Connecting()
        {
            TcpClient = new TcpClient();
            while (true)
            {
                Thread.Sleep(5000);
                if (!IsConnected)
                {
                    if (!TryConnect())
                        Thread.Sleep(5000);
                    else
                        Connected?.Invoke();
                }
            }
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
