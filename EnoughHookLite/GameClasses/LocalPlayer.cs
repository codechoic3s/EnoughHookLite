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
        public LocalPlayer(App app) : base(app, 0)
        {
        }

        public int CrosshairID { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_iCrosshairId); } }
        public Vector3 AimPunchAngle { get { return App.Client.ClientModule.ReadStruct<Vector3>(Pointer + Offsets.csgo.netvars.m_aimPunchAngle); } }
        public Vector3 Velocity { get { return App.Client.ClientModule.ReadStruct<Vector3>(Pointer + Offsets.csgo.netvars.m_vecVelocity); } }
        public float Speed { get { return (float)Math.Sqrt((double)(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y + Velocity.Z * Velocity.Z)); } }

        public float Fov
        {
            get
            {
                var fov = App.Client.ClientModule.ReadFloat(Pointer + Offsets.csgo.netvars.m_iFOV);
                if (fov == 0)
                    fov = 90;
                return fov;
            }
        }
    }
}
