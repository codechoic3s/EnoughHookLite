using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Features
{
    public class MagnetTrigger
    {
        public App App;
        public Thread TH;
        public ManualResetEvent MRE;
        public TimeSpan TimeSpan;

        public MagnetTrigger(App app)
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
            while (true)
            {
                MRE.WaitOne(TimeSpan);
                if (App.CanNext && App.ConfigManager.CurrentConfig.Trigger.Enabled && Process.GetKeyState(App.ConfigManager.CurrentConfig.Trigger.Button))
                {
                    var triggered = App.Client.GetFovPlayer(1);
                    var head = triggered.GetBonePosition(0);
                    var d2 = App.Engine.WorldToScreen(head);
                    if (triggered != null)
                    {
                        App.Process.MoveCursor(d2, 0.1f, 0.1f);
                        App.Engine.Fire(true);
                        App.Engine.Fire(false);
                    }
                }
            }
        }
    }
}
