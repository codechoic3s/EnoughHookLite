using EnoughHookLite.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class EntityList
    {
        public LocalPlayer LocalPlayer;

        public List<CSPlayer> CSPlayers;
        public App App;

        private Thread TH;

        public EntityList(App app)
        {
            App = app;
            CSPlayers = new List<CSPlayer>();
        }

        public CSPlayer GetByCrosshairID(int id)
        {
            return CSPlayers[id];
        }

        public void Start()
        {
            TH = new Thread(new ThreadStart(Work));
            TH.Start();
        }

        private void Work()
        {
            LocalPlayer = new LocalPlayer(App);
            while (true)
            {
                // filling csplayers
                if (CSPlayers.Count < 64)
                {
                    for (var i = 0; i < 64; i++)
                    {
                        CSPlayers.Add(new CSPlayer(App, i));
                    }
                }

                // updating csplayers
                for (var i = 0; i < 64; i++)
                {
                    var ptr = App.Client.ClientModule.ReadInt(App.Client.ClientModule.BaseAdr + Offsets.csgo.signatures.dwEntityList + (i * 0x10));
                    var csp = CSPlayers[i];
                    if (csp.Pointer != ptr && ptr != 0)
                    {
                        csp.Pointer = ptr;
                    }
                    if (csp.Pointer == LocalPlayer.Pointer)
                        LocalPlayer.Index = csp.Index;
                }

                // updating localplayer
                var localptr = App.Client.ClientModule.ReadInt(App.Client.ClientModule.BaseAdr + Offsets.csgo.signatures.dwLocalPlayer);
                if (LocalPlayer.Pointer != localptr)
                    LocalPlayer.Pointer = localptr;

                Thread.Sleep(1000);
            }
        }
    }
}
