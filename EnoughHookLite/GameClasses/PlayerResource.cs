using EnoughHookLite.Modules;
using EnoughHookLite.Offsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class PlayerResource
    {
        public int Pointer { get; internal set; }
        public bool IsWorking { get; private set; }

        private Client Client;

        public PlayerResource(Client client)
        {
            Client = client;
        }

        internal async void RunTask()
        {
            IsWorking = true;
            while (IsWorking)
            {
                Pointer = Client.NativeModule.ReadInt(Client.NativeModule.BaseAdr + App.OffsetLoader.Offsets.Signatures.dwPlayerResource);
                await Task.Delay(5000);
            }
        }
        internal void Stop()
        {
            IsWorking = false;
        }

        public int GetWins(int csplayer_index)
        {
            return Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_iCompetitiveWins + csplayer_index * 4);
        }
        public int GetWins(CSPlayer csplayer)
        {
            return Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_iCompetitiveWins + csplayer.Index * 4);
        }
        public Rank GetRank(CSPlayer csplayer)
        {
            return (Rank)Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_iCompetitiveRanking + csplayer.Index * 4);
        }
        public Rank GetRank(int csplayer_index)
        {
            return (Rank)Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_iCompetitiveRanking + csplayer_index * 4);
        }
    }
}
