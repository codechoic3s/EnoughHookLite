using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Features
{
    public class Trigger
    {
        public Unstable Unstable;
        public Thread TH;
        public ManualResetEvent MRE;
        public TimeSpan TimeSpan;
        

        public Trigger(Unstable unstable)
        {
            Unstable = unstable;
            MRE = new ManualResetEvent(false);
            TimeSpan = TimeSpan.FromMilliseconds(0.5);
        }

        public void Start()
        {
            TH = new Thread(new ThreadStart(Work));
            TH.Start();
        }

        private void Work()
        {
            Parallel.Invoke(() =>
            {
                while (true)
                {
                    MRE.WaitOne(TimeSpan);
                    //Thread.Sleep(1);
                    if (Unstable.GetKeyState(Unstable.VK.LSHIFT))
                    {
                        var team = Unstable.RPMInt((IntPtr)Unstable.LocalPlayerInstance + Unstable.TeamIDPTR);
                        var crossInd = Unstable.RPMInt((IntPtr)Unstable.LocalPlayerInstance + Unstable.CrosshairIDPTR);
                        var crossEnt = Unstable.RPMInt(Unstable.ClientModulePtr + Unstable.EntityListPTR + (crossInd - 1) * 16);
                        var crossTeam = Unstable.RPMInt((IntPtr)crossEnt + Unstable.TeamIDPTR);

                        if (crossInd < 64 && crossInd > 0 && crossTeam != team)
                        {
                            Unstable.Fire(true);
                            Unstable.Fire(false);
                        }
                    }
                }
            });
        }
    }
}
