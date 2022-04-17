using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Sys
{
    public sealed class CompileCache<TObj>
    {
        public TObj Value => ComputeValue();

        public Func<TObj> ComputeValue;

        public CompileCache(Func<TObj> func)
        {
            ComputeValue = func;
        }

        public static implicit operator CompileCache<TObj>(Func<TObj> func)
        {
            return new CompileCache<TObj>(func);
        }
    }
}
