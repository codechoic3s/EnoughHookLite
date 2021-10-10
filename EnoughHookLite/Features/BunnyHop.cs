using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Features
{
    public class BunnyHop
    {
        public Thread TH;
        public Unstable Unstable;

        public BunnyHop(Unstable u)
        {
            Unstable = u;
        }

        public void Start()
        {
            TH = new Thread(new ThreadStart(Work));
            TH.Start();
        }

        private void Work()
        {
            while (true)
            {
                Thread.Sleep(1);
                if (Unstable.GetKeyState(Unstable.VK.SPACE))
                {
                    var flags = Unstable.RPMInt((IntPtr)Unstable.LocalPlayerInstance + Unstable.FlagsPTR);
                    if (flags == 257)
                    {
                        Unstable.Jump(true);
                        Unstable.Jump(false);
                    }
                }
            }
        }
    }
}
