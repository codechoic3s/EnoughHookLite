using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Modules
{
    public class ManagedModule
    {
        public Module NativeModule { get; private set; }

        public ManagedModule(Module nm)
        {
            NativeModule = nm;
        }
    }
}
