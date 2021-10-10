using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class CSPlayer : Entity
    {
        public CSPlayer(App app) : base(app)
        {
        }

        public int FFlags { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_fFlags); } }
        public Team Team { get { return (Team)App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_iTeamNum); } }
        public int Armor { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_ArmorValue); } }
        public int Health { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_iHealth); } }
        public bool HasHelmet { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bHasHelmet); } }
        public bool HasDefuseKit { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bHasDefuser); } }
        public bool IsDefusing { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bIsDefusing); } }
        public bool InReload { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bInReload); } }
        public bool Spotted { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bSpotted); } }
        public bool SpottedByMask { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bSpottedByMask); } }
        public bool IsScoped { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bIsScoped); } }
        public int Clip1 { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_iClip1); } }

        public bool IsPlayer { get { var tm = (int)Team; bool ok = ((tm > 0) && (tm < 5)); return ok; } }
    }
}
