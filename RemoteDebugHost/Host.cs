using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteDebugHost
{
    public sealed class Host
    {
        private Thread NativeThread;
        private TcpListener TcpListener;

        private NetworkStream NetworkStream;
        private TcpClient Client;
        public bool IsWorking { get; private set; }
        public Func<int> OnGetPort;
        public int DefaultPort;
        public int Port { get; private set; }

        public Action<string> OnNewConnection;
        public Action<string> OnConnectionInterrupt;
        public Action OnConnectionEnd;
        public Action<string> OnMessage;
        public Action OnWaitingConnection;

        public Host()
        {
            DefaultPort = 8888;
        }

        public void Start()
        {
            if (!IsWorking)
            {
                IsWorking = true;
                NativeThread = new Thread(Work);
                NativeThread.Start();
            }
        }
        /// <summary>
        /// Set signal for stop.
        /// </summary>
        public void Stop()
        {
            IsWorking = false;
        }

        private void Work()
        {
            while (IsWorking)
            {
                Port = OnGetPort is null ? DefaultPort : OnGetPort();
                Accept();
                Maintain();
            }
            IsWorking = false;
        }

        private void Accept()
        {
            TcpListener = new TcpListener(IPAddress.Any, Port);
            TcpListener.Start();
            OnWaitingConnection?.Invoke();

            Client = TcpListener.AcceptTcpClient();
            NetworkStream = Client.GetStream();

            OnNewConnection?.Invoke(((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString());

            TcpListener.Stop();
        }

        private void Maintain()
        {
            try
            {
                while (IsWorking)
                {
                    byte[] buf = new byte[1024];
                    StringBuilder sb = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = NetworkStream.Read(buf, 0, buf.Length);
                        sb.Append(Encoding.Unicode.GetString(buf, 0, bytes));
                    }
                    while (NetworkStream.DataAvailable);

                    OnMessage?.Invoke(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                OnConnectionInterrupt?.Invoke(ex.Message);
            }
            OnConnectionEnd?.Invoke();
        }
    }
}
