using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public interface IUnmanagedObject
    {
        uint Pointer { get; }
        bool Computed { get; }
        void Compute(uint pointer);
    }
}
