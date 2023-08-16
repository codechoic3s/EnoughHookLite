using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing
{
    public class PointerCached
    {
        public uint Pointer { get; private set; }

        public PointerCached(uint pointer)
        {
            Pointer = pointer;
        }
    }
}
