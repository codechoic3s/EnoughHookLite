using EnoughHookLite.Modules;
using EnoughHookLite.Pointing;
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
            AllocatePointers();

        }

        private void LogIt(string log)
        {
            Console.WriteLine("[PlayerResource] " + log);
        }

        private PointerCached pPlayerResource;
        private PointerCached pCompetitiveWins;
        private PointerCached pCompetitiveRanking;

        private void AllocatePointers()
        {
            if (!Client.SubAPI.PointManager.AllocateNetvar("DT_CSPlayerResource.m_iCompetitiveRanking", out pCompetitiveRanking))
            {
                LogIt("Failed get CompetitiveRanking");
                return;
            }
            if (!Client.SubAPI.PointManager.AllocateNetvar("DT_CSPlayerResource.m_iCompetitiveWins", out pCompetitiveWins))
            {
                LogIt("Failed get CompetitiveWins");
                return;
            }
            if (!Client.SubAPI.PointManager.AllocateSignature(SignaturesConsts.dwPlayerResource, out pCompetitiveWins))
            {
                LogIt("Failed get CompetitiveWins");
                return;
            }
        }

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
