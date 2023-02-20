using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration
{
    public interface ISharedVarsHandler
    {
        void AddDelegate(string name, Delegate del);

        void AddValue(string name, object obj);
    }
}
