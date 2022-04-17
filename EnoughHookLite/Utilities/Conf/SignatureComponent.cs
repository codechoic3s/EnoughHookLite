using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class SignatureComponent
    {
        public string Signature { get; set; }
        public int[] Offsets { get; set; }
        public int Extra { get; set; }
        public bool Relative { get; set; }
        public string Module { get; set; }

        public SignatureComponent()
        {
            Signature = "";
            Offsets = new int[] { 0 };
            Extra = 0;
            Relative = false;
            Module = "";
        }
    }
}
