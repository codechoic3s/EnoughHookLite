using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Apis.APIWraps
{
    public sealed class ScriptRemoteMemory
    {
        private RemoteMemory RemoteMemory;
        public ScriptRemoteMemory(RemoteMemory rm)
        {
            RemoteMemory = rm;
        }

        public int ReadIntInt(double adr)
        {
            return RemoteMemory.ReadInt((int)adr);
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
        public int ReadIntUInt(double adr)
        {
            return RemoteMemory.ReadInt((uint)adr);
        }
    }
}
