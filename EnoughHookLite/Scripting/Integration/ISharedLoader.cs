using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration
{
    public interface ISharedLoader
    {
        void AddSharedAPI(string name, SharedAPI api);
    }
}
