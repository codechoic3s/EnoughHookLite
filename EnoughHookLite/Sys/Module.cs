using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Sys
{
    public class Module
    {
        public Process Process { get; private set; }

        public uint BaseAdr { get; private set; }
        public uint Size { get; private set; }
        public uint SizeAdr { get; private set; }
        public string Name { get; private set; }

        private System.Diagnostics.ProcessModule pModule;

        public Module(System.Diagnostics.ProcessModule pm, Process proc)
        {
            pModule = pm;
            Process = proc;
            BaseAdr = (uint)pm.BaseAddress;
            Size = (uint)pm.ModuleMemorySize;
            SizeAdr = (uint)(BaseAdr + Size);
            Name = pm.ModuleName;
        }
    }
}
