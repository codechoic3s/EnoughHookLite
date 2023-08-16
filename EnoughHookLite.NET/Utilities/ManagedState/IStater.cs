using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.ManagedState
{
    public interface IStater
    {
        void Stop();
        void Start();
        bool IsWorking { get; }
    }
}
