using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.OtherCode.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ClientClass
    {
        [FieldOffset(0x08)]
        public int pNetworkName;
        [FieldOffset(0x0C)]
        public int pRecvTable;
        [FieldOffset(0x10)]
        public int pNext;
        [FieldOffset(0x14)]
        public int ClassID;

        public static ClientClass Read(RemoteMemory rm, int ptr)
        {
            return new ClientClass
            {
                pNetworkName = rm.ReadInt(ptr + 0x08),
                pRecvTable = rm.ReadInt(ptr + 0x0C),
                pNext = rm.ReadInt(ptr + 0x10),
                ClassID = rm.ReadInt(ptr + 0x14)
            };
        }
    }
}
