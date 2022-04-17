using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.OtherCode.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct RecvProp_t
    {
        [FieldOffset(0x0)]
        public int pVarName;
        [FieldOffset(0x4)]
        public ePropType RecvType;
        [FieldOffset(0x8)]
        public int Flags;
        [FieldOffset(0xC)]
        public int StringBufferSize;
        [FieldOffset(0x10)]
        public bool bInsideArray;
        [FieldOffset(0x14)]
        public int pExtraData;
        [FieldOffset(0x18)]
        public int pArrayProp;
        [FieldOffset(0x1C)]
        public int mrrayLengthProxy;
        [FieldOffset(0x20)]
        public int ProxyFn;
        [FieldOffset(0x24)]
        public int DataTableProxyFn;
        [FieldOffset(0x28)]
        public int pDataTable;
        [FieldOffset(0x2C)]
        public int Offset;
        [FieldOffset(0x30)]
        public int ElementStride;
        [FieldOffset(0x34)]
        public int nElements;
        [FieldOffset(0x38)]
        public int pParentArrayPropName;

        public int GetPropTypeSize()
        {
            return GetPropTypeSize(RecvType);
        }

        public static int GetPropTypeSize(ePropType type)
        {
            switch (type)
            {
                case ePropType.Int:
                case ePropType.Float:
                    return 4;
                case ePropType.VectorXY:
                case ePropType.Int64:
                    return 8;
                case ePropType.Vector:
                    return 12;
            }
            return -1;
        }
    }
}
