using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration.Apis.APIWraps
{
    public sealed class ScriptRemoteMemory
    {
        private RemoteMemory RemoteMemory;
        public ScriptRemoteMemory(RemoteMemory rm)
        {
            RemoteMemory = rm;
        }
        public Vector3 ReadVector3UInt(double adr)
        {
            return ReadStructUInt<Vector3>(adr);
        }
        public T ReadStructUInt<T>(double adr)
        {
            return RemoteMemory.ReadStruct<T>((uint)adr);
        }
        public uint ReadUIntUInt(double adr)
        {
            return RemoteMemory.ReadUInt((uint)adr);
        }
        public int ReadIntIntptr(double adr)
        {
            return RemoteMemory.ReadInt((IntPtr)adr);
        }
        public int ReadIntLong(double adr)
        {
            return RemoteMemory.ReadInt((long)adr);
        }
        public int ReadIntUlong(double adr)
        {
            return RemoteMemory.ReadInt((ulong)adr);
        }
    }
}
