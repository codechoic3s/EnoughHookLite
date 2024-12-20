﻿using EnoughHookLite.OtherCode.Structs;
using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.ClientClassManaging
{
    public sealed class ManagedRecvProp : IUnmanagedObject
    {
        public uint Pointer { get; private set; }
        public bool Computed { get; private set; }

        public CompileCache<RecvProp_t> RecvProp { get; private set; }

        public CompileCache<string> VarName { get; private set; }

        public uint Offset => RecvProp.Value.Offset;
        public uint StringBufferSize => RecvProp.Value.StringBufferSize;
        public ePropType PropType => RecvProp.Value.RecvType;

        public CompileCache<ManagedRecvTable> SubTable { get; private set; }
        public CompileCache<int> BaseClassDepth { get; private set; }
        public CompileCache<uint> Size { get; private set; }
        public ManagedRecvTable Table { get; private set; }
        public CompileCache<ManagedRecvProp[]> ArrayProp { get; private set; }
        public uint ElementCount => RecvProp.Value.nElements;

        private RemoteMemory RemoteMemory;
        public ManagedRecvProp(RemoteMemory rm, ManagedRecvTable table) { RemoteMemory = rm; Table = table; }

        public void Compute(uint pointer)
        {
            Pointer = pointer;

            RecvProp = new CompileCache<RecvProp_t>(() => RemoteMemory.ReadStruct<RecvProp_t>(Pointer));
            VarName = new CompileCache<string>(() => RemoteMemory.ReadString(RecvProp.Value.pVarName, 32, Encoding.ASCII));

            SubTable = new CompileCache<ManagedRecvTable>(() =>
            {
                uint pdatatable = RecvProp.Value.pDataTable;
                if (pdatatable != 0)
                {
                    var subtable = new ManagedRecvTable(RemoteMemory);
                    subtable.Compute(pdatatable);
                    return subtable;
                }
                return null;
            });

            BaseClassDepth = new CompileCache<int>(() =>
            {
                var subtable = SubTable.Value;
                return subtable is null ? 0 : 1 + subtable.BaseClassDepth.Value;
            });

            ArrayProp = new CompileCache<ManagedRecvProp[]>(GetProps);

            Size = new CompileCache<uint>(GetSize);
            
            Computed = true;
        }

        private ManagedRecvProp[] GetProps()
        {
            var recvprop = RecvProp.Value;

            var elementscount = recvprop.nElements;
            var parrayprop = recvprop.pArrayProp;
            if (recvprop.RecvType != ePropType.Array || parrayprop == 0 || elementscount == 0)
                return new ManagedRecvProp[0];

            var props = new ManagedRecvProp[elementscount];
            uint size = (uint)Marshal.SizeOf<RecvProp_t>();
            for (uint i = 0; i < elementscount; i++)
            {
                uint a = parrayprop + size * i;
                if (a == Pointer)
                    props[i] = this;
                else
                {
                    var prop = new ManagedRecvProp(RemoteMemory, Table);
                    prop.Compute(a);
                    props[i] = prop;
                }
            }
            return props;
        }

        private uint GetSize()
        {
            var subtable = SubTable.Value;

            if (subtable != null)
                return subtable.Size.Value;

            int size = RecvProp.Value.GetPropTypeSize();
            if (size >= 0)
                return (uint)size;

            switch (PropType)
            {
                case ePropType.String:
                    {
                        return StringBufferSize;
                    }
                case ePropType.Array:
                    {
                        var arrayprop = ArrayProp.Value;
                        if (arrayprop.Length > 0)
                            return ElementCount * arrayprop[0].Size.Value;
                        return 0;
                    }
            }
            return 0;
        }
    }
}
