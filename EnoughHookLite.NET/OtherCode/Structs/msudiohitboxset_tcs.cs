using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.OtherCode.Structs
{
    /// <summary>
    /// https://github.com/ValveSoftware/source-sdk-2013/blob/0d8dceea4310fde5706b3ce1c70609d72a38efdf/sp/src/public/studio.h#L1612
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct mstudiohitboxset_t
    {
        public int sznameindex;
        public int numhitboxes;
        public int hitboxindex;
    }
}
