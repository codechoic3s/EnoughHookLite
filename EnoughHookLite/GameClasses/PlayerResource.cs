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
        public int Pointer;
        public Client Client;
        private ManualResetEvent MRE;
        public TimeSpan TimeSpan;

        private Thread TH;

        public PlayerResource(Client client)
        {
            Client = client;
            MRE = new ManualResetEvent(false);
            TimeSpan = TimeSpan.FromSeconds(5);
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
                Pointer = Client.ClientModule.ReadInt(Client.ClientModule.BaseAdr + Offsets.csgo.signatures.dwPlayerResource);
            }
        }

        public int GetWins(int csplayer_index)
        {
            return Client.ClientModule.ReadInt(Pointer + csgo.netvars.m_iCompetitiveWins + csplayer_index * 4);
        }
        public int GetWins(CSPlayer csplayer)
        {
            return Client.ClientModule.ReadInt(Pointer + csgo.netvars.m_iCompetitiveWins + csplayer.Index * 4);
        }
        public Rank GetRank(CSPlayer csplayer)
        {
            return (Rank)Client.ClientModule.ReadInt(Pointer + csgo.netvars.m_iCompetitiveRanking + csplayer.Index * 4);
        }
        public Rank GetRank(int csplayer_index)
        {
            return (Rank)Client.ClientModule.ReadInt(Pointer + csgo.netvars.m_iCompetitiveRanking + csplayer_index * 4);
        }
    }
}
