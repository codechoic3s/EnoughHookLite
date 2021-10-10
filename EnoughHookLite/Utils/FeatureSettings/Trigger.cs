using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utils.FeatureSettings
{
    public struct Trigger
    {
        public bool Enabled;
        public VK Button;

        public Trigger(VK button = VK.LSHIFT)
        {
            Enabled = true;
            Button = button;
        }
    }
}
