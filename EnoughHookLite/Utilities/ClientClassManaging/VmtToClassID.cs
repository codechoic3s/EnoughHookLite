using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.ClientClassManaging
{
    public sealed class VmtToClassID
    {
        private Dictionary<int, ManagedClientClass> dict;

        public static VmtToClassID Instance { get; private set; } = new VmtToClassID();

        public ManagedClientClass this[int address]
        {
            get
            {
                return dict[address];
            }
            set
            {
                dict[address] = value;
            }
        }

        private VmtToClassID()
        {
            dict = new Dictionary<int, ManagedClientClass>();
        }

        public bool ContainsVMT(int address)
        {
            return dict.ContainsKey(address);
        }
    }
}
