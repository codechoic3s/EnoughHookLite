using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class LocalPlayer : CSPlayer
    {
        public LocalPlayer(App app) : base(app)
        {
        }

        public int CrosshairID { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_iCrosshairId); } }
        public Vector3 AimPunchAngle { get { return App.Client.ClientModule.ReadStruct<Vector3>(Pointer + Offsets.csgo.netvars.m_aimPunchAngle); } }
    }
}
