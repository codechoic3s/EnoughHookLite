using EnoughHookLite.Utils;
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
        public Vector2 AimPunchAngle { get { return App.Client.ClientModule.ReadStruct<Vector2>(Pointer + Offsets.csgo.netvars.m_aimPunchAngle); } }
        public Vector3 Velocity { get { return App.Client.ClientModule.ReadStruct<Vector3>(Pointer + Offsets.csgo.netvars.m_vecVelocity); } }
        public float Speed { get { return (float)System.Math.Sqrt((double)(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y + Velocity.Z * Velocity.Z)); } }

        public Vector3 VecPosition { get { return App.Client.ClientModule.ReadStruct<Vector3>(Pointer + Offsets.csgo.netvars.m_vecViewOffset); } }
        public Vector3 EyePosition { get { return VecPosition + VecOrigin; } }

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

        public Vector3 GetAimDirection()
        {
            var va = App.Client.ClientState_ViewAngles;
            var apa = AimPunchAngle;
            var phi = (va.X + apa.X * 2.0).DegreeToRadian();
            var theta = (va.Y + apa.Y * 2.0).DegreeToRadian();

            // https://en.wikipedia.org/wiki/Spherical_coordinate_system
            return new Vector3
            (
                (float)(System.Math.Cos(phi) * System.Math.Cos(theta)),
                (float)(System.Math.Cos(phi) * System.Math.Sin(theta)),
                (float)-System.Math.Sin(phi)
            );
        }
    }
}
