using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing
{
    public sealed class PointerCached
    {
        public int Pointer { get; private set; }

        public PointerCached(int p)
        {
            Pointer = p;
        }
    }
}
