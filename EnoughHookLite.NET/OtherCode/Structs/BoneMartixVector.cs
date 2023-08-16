using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.OtherCode.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct BoneMartixVector
    {
        [FieldOffset(0x0C)]
        public float X;
        [FieldOffset(0x1C)]
        public float Y;
        [FieldOffset(0x2C)]
        public float Z;
    }
}
