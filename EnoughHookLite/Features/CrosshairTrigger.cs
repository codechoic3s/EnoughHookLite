using EnoughHookLite.Modules;
using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Features
{
    public class CrosshairTrigger
    {
        public App App;
        public Thread TH;
        public ManualResetEvent MRE;
        public TimeSpan TimeSpan;

        public CrosshairTrigger(App app)
        {
            App = app;
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
                    if (App.CanNext && App.ConfigManager.CurrentConfig.Trigger.Enabled && Process.GetKeyState(App.ConfigManager.CurrentConfig.Trigger.Button))
                    {
                        var crossInd = App.Client.EntityList.LocalPlayer.CrosshairID;

                        if (crossInd < 64 && crossInd > 0) 
                        {
                            var crossEnt = App.Client.EntityList.GetByCrosshairID(crossInd - 1);

                            if (crossEnt.Team != App.Client.EntityList.LocalPlayer.Team)
                            {
                                App.Engine.Fire(true);
                                App.Engine.Fire(false);
                            }
                        }
                    }
                }
            });
        }
    }
}
