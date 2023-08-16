using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Sys
{
    public sealed class Signature
    {
        public ulong Id;
        public string Name;
        public short[] Sig;
        public int[] Offsets;
        public uint Extra;
        public bool Relative;

        public uint Pointer;
        public bool Finded = false;
        public Module Module;

        public Signature(Module m, params short[] signature)
        {
            Sig = signature;
            Module = m;
        }
        public Signature(Module m, int[] offsets, uint extra, bool relative, string name, ulong id, params short[] signature)
        {
            Sig = signature;
            Offsets = offsets;
            Extra = extra;
            Relative = relative;
            Name = name;
            Id = id;
            Module = m;
        }

        /*
        public static implicit operator Signature(ValueTuple<Module, short[]> val)
        {
            return new Signature(val.Item1, val.Item2);
        }

        public static implicit operator Signature(ValueTuple<Module, int[], int, bool, short[]> val)
        {
            return new Signature(val.Item1, val.Item2, val.Item3, val.Item4, val.Item5);
        }
        */
    }
}
