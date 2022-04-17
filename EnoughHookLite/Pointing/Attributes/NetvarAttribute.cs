using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing.Attributes
{
    public sealed class NetvarAttribute : Attribute
    {
        public string NameSpace { get; private set; }

        public NetvarAttribute(string ns)
        {
            NameSpace = ns;
        }
    }
}
