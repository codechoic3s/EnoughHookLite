using EnoughHookLite.Pointing;
using EnoughHookLite.Pointing.Attributes;
using EnoughHookLite.Utilities;
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
        public const string ClassName = "DT_LocalPlayerExclusive";
        public LocalPlayer(SubAPI api, uint ptr) : base(api, ptr, 0)
        {
        }

        //[Netvar(ClassName + ".m_iCrosshairId")]
        //private PointerCached pCrosshairID;
        //[Netvar(ClassName + ".m_iCrosshairId")]
        //private PointerCached pVelocity;
        [Netvar(ClassName + ".m_iCrosshairId")]
        private PointerCached pFov;

        //public int CrosshairID { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_iCrosshairId); } }
        //public Vector2 AimPunchAngle { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadStruct<Vector2>(Pointer + App.OffsetLoader.Offsets.Netvars.m_aimPunchAngle); } }
        //public Vector3 Velocity { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadStruct<Vector3>(Pointer + App.OffsetLoader.Offsets.Netvars.m_vecVelocity); } }
        //public float Speed { get { return (float)System.Math.Sqrt(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y + Velocity.Z * Velocity.Z); } }

        //public Vector3 VecPosition { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadStruct<Vector3>(Pointer + App.OffsetLoader.Offsets.Netvars.m_vecViewOffset); } }
        //public Vector3 EyePosition { get { return VecPosition + VecOrigin; } }

        public float Fov
        {
            get
            {
                var fov = SubAPI.Client.NativeModule.Process.RemoteMemory.ReadFloat(Pointer + pFov.Pointer);
                if (fov == 0)
                    fov = 90;
                return fov;
            }
        }
        /*
        public Vector3 GetAimDirection()
        {
            var va = SubAPI.Engine.ClientState_ViewAngles;
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
        */
    }
}
