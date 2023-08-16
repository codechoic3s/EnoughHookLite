using EnoughHookLite.Modules;
using EnoughHookLite.Pointing;
using EnoughHookLite.Pointing.Attributes;
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
        private const string ClassName = "DT_CSPlayerResource";

        public int Pointer { get; internal set; }
        public bool IsWorking { get; private set; }

        private Client Client;

        public PlayerResource(Client client)
        {
            Client = client;
        }

        [Signature(SignaturesConsts.dwPlayerResource)]
        private PointerCached pPlayerResource;
        [Netvar(ClassName + ".m_iCompetitiveWins")]
        private PointerCached pCompetitiveWins;
        [Netvar(ClassName + ".m_iCompetitiveRanking")]
        private PointerCached pCompetitiveRanking;

        internal async void FetchMemoryAddress()
        {
            IsWorking = true;
            while (IsWorking)
            {
                Pointer = Client.NativeModule.Process.RemoteMemory.ReadInt(Client.NativeModule.BaseAdr + pPlayerResource.Pointer);
                await Task.Delay(5000);
            }
        }
        internal void Stop()
        {
            IsWorking = false;
        }
        public int GetWins(int csplayer_index)
        {
            return Client.NativeModule.Process.RemoteMemory.ReadInt(Pointer + pCompetitiveWins.Pointer + csplayer_index * 4);
        }
        public int GetWins(CSPlayer csplayer)
        {
            return Client.NativeModule.Process.RemoteMemory.ReadInt(Pointer + pCompetitiveWins.Pointer + csplayer.Index * 4);
        }
        public Rank GetRank(CSPlayer csplayer)
        {
            return (Rank)Client.NativeModule.Process.RemoteMemory.ReadInt(Pointer + pCompetitiveRanking.Pointer + csplayer.Index * 4);
        }
        public Rank GetRank(int csplayer_index)
        {
            return (Rank)Client.NativeModule.Process.RemoteMemory.ReadInt(Pointer + pCompetitiveRanking.Pointer + csplayer_index * 4);
        }
    }
}
