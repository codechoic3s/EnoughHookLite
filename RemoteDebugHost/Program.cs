using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDebugHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new Host();
            host.Start();
        }
    }
}
