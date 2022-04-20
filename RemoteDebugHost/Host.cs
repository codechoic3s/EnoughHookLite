using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDebugHost
{
    public sealed class Host
    {
        private Thread NativeThread;
        private TcpListener TcpListener;

        private NetworkStream NetworkStream;
        private TcpClient Client;
        private int Port;
        public void Start()
        {
            NativeThread = new Thread(Work);
            NativeThread.Start();
        }

        private void Work()
        {
            Port = Convert.ToInt32(Ask("input port: "));
            while (true)
            {
                Accept();
                Maintain();
            }
        }

        private ConsoleKeyInfo AskKey(string str)
        {
            Console.Write(str);
            return Console.ReadKey();
        }
        private string Ask(string str)
        {
            Console.Write(str);
            return Console.ReadLine();
        }

        private void Accept()
        {
            Console.Clear();
            Console.Title = $"Waiting on port: {Port}";
            TcpListener = new TcpListener(System.Net.IPAddress.Any, Port);
            TcpListener.Start();

            Client = TcpListener.AcceptTcpClient();
            NetworkStream = Client.GetStream();

            Console.Title = $"Connection on {Client.Client.AddressFamily} (host_port={Port})";

            TcpListener.Stop();
        }

        private void Maintain()
        {
            try
            {
                while (true)
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
                    
                    Console.WriteLine(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Title += " CONNECTION_INTERUPTED";
            }
            AskKey("Any key for host again...");
        }
    }
}
