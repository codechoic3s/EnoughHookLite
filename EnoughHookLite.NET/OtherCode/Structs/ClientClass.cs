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
        public uint pNetworkName;
        [FieldOffset(0x0C)]
        public uint pRecvTable;
        [FieldOffset(0x10)]
        public uint pNext;
        [FieldOffset(0x14)]
        public int ClassID;

        public static ClientClass Read(RemoteMemory rm, uint ptr)
        {
            return new ClientClass
            {
                pNetworkName = rm.ReadUInt(ptr + 0x08),
                pRecvTable = rm.ReadUInt(ptr + 0x0C),
                pNext = rm.ReadUInt(ptr + 0x10),
                ClassID = rm.ReadInt(ptr + 0x14)
            };
        }
    }
}
