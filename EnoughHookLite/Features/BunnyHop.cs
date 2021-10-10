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
    public class BunnyHop
    {
        public Thread TH;

        public App App;

        public BunnyHop(App app)
        {
            App = app;
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
                if (Process.GetKeyState(VK.SPACE))
                {
                    if (App.Client.EntityList.LocalPlayer.FFlags == 257 || App.Client.EntityList.LocalPlayer.FFlags == 263)
                    {
                        App.Engine.Jump(true);
                        App.Engine.Jump(false);
                    }
                }
            }
        }
    }
}
