using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnstableTrigger
{
    class Program
    {
        static void Main(string[] args)
        {
            var unstable = new Unstable();
            var state = unstable.AllocateProcess();
            unstable.Start();
            if (state)
            {
                unstable.LoadOffsets();
                var trigger = new Trigger(unstable);
                trigger.Start();
                var bhop = new BunnyHop(unstable);
                bhop.Start();
                Console.WriteLine("Process finded.");
            }
            else
            {
                Console.WriteLine("Unknown process.");
            }
        }
    }
}
