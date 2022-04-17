using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public interface IUnmanagedObject
    {
        int Pointer { get; }
        bool Computed { get; }
        void Compute(int pointer);
    }
}
