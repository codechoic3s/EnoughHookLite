using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public abstract class SharedAPI
    {
        internal abstract void SetupAPI(ScriptLocal local);
    }
}
