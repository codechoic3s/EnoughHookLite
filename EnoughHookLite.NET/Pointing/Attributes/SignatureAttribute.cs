using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing.Attributes
{
    public sealed class SignatureAttribute : Attribute
    {
        public ulong Id { get; private set; }

        public SignatureAttribute(ulong id)
        {
            Id = id;
        }
        public SignatureAttribute(SignaturesConsts c)
        {
            Id = (ulong)c;
        }
    }
}
