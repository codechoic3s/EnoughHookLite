using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.OtherCode.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct RecvTable_t
    {
        [FieldOffset(0x0)]
        public int pProps;
        [FieldOffset(0x4)]
        public int nProps;
        //[FieldOffset(0x8)]
        //EMTPY!
        [FieldOffset(0xC)]
        public int pNetTableName;
    }
}
