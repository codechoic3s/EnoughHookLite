using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing
{
    public sealed class SpecialPointerCached : PointerCached
    {
        public int Offset { get; private set; }

        public SpecialPointerCached(int pointer, int offset) : base(pointer)
        {
            Offset = offset;
        }
    }
}
