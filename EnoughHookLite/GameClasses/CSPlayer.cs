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

        public bool IsPlayer { get { var tm = (int)Team; bool ok = ((tm > 0) && (tm < 5)); return ok; } }
    }
}
