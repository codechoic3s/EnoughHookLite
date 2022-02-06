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
        public LocalPlayer LocalPlayer { get; private set; }
        public bool IsWorking { get; private set; }

        internal Dictionary<int, CSPlayer> CSPlayers;
        private App App;

        public EntityList(App app)
        {
            App = app;
            CSPlayers = new Dictionary<int, CSPlayer>();
        }

        public CSPlayer GetByCrosshairID(int id)
        {
            return CSPlayers[id];
        }

        internal async void RunTask()
        {
            IsWorking = true;
            LocalPlayer = new LocalPlayer(App);
            while (IsWorking)
            {
                int maxplayers = App.Engine.ClientState_MaxPlayers;
                int csc = CSPlayers.Count;

                // filling csplayers
                if (csc < maxplayers)
                {
                    int ost = (csc - maxplayers) + maxplayers;
                    for (int i = ost; i < maxplayers; i++)
                    {
                        CSPlayers.Add(i, new CSPlayer(App, i));
                    }
                }
                else if (csc > maxplayers)
                {
                    //int ost = csc - maxplayers;
                    CSPlayers.Clear();
                    //CSPlayers.RemoveRange(csc - 1, ost - 1);
                }

                // updating csplayers
                int elist = App.Client.NativeModule.BaseAdr + App.OffsetLoader.Offsets.Signatures.dwEntityList;
                for (int i = 0; i < maxplayers; i++)
                {
                    int ptr = App.Client.NativeModule.ReadInt(elist + (i * 0x10));
                    CSPlayer csp = CSPlayers[i];
                    if (ptr != 0 && csp.Pointer != ptr)
                    {
                        csp.Pointer = ptr;
                    }
                    //if (ptr == 0)
                        //App.Log.LogIt($"{i} {ptr} null");
                }

                if (CSPlayers.Count > 0) // fix
                {
                    // updating localplayer
                    int localindx = App.Engine.ClientState_GetLocalPlayer;
                    //var localptr = App.Client.ClientModule.ReadInt(App.Client.ClientModule.BaseAdr + Offsets.App.OffsetLoader.Offsets.Signatures.dwLocalPlayer);
                    if (localindx < CSPlayers.Count)
                        LocalPlayer.Pointer = CSPlayers[localindx].Pointer;
                }

                await Task.Delay(3000);
                //Thread.Sleep(3000);
            }
        }
    }
}
