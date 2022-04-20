using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.OtherCode.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct CEntInfo
    {
        [FieldOffset(0x00)]
        public int pEntity;
        [FieldOffset(0x04)]
        public int SerialNumber;
        [FieldOffset(0x08)]
        public int pPrevious;
        [FieldOffset(0x0C)]
        public int pNext;

        public override string ToString()
        {
            return $"pEntity={pEntity} SerialNumber={SerialNumber} pPrevious={pPrevious} pNext={pNext}";
        }
    }
}
