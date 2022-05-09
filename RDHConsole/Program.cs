using RemoteDebugHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDHConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new Host();
            var port = Convert.ToInt32(Ask("Port: "));
            host.OnGetPort = () => { return port; };

            host.OnMessage = (str) => { Console.WriteLine(str); };
            host.OnNewConnection = (ip) => { Console.Title = $"Connection on {ip} (host on {port})"; };
            host.OnConnectionEnd = () => { AskKey("Any key to wait next connection..."); };
            host.OnConnectionInterrupt = (info) => { Console.Title += " +CONNECTION INTERRUPTED+"; Console.WriteLine(info); };
            host.OnWaitingConnection = () => { Console.Clear(); Console.Title = $"Waiting new connection on {port}"; };

            host.Start();
        }

        static string Ask(string a)
        {
            Console.Write(a);
            return Console.ReadLine();
        }
        static ConsoleKeyInfo AskKey(string a)
        {
            Console.Write(a);
            return Console.ReadKey();
        }
    }
}
