using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class SignaturesConfig : SerializableConf<SignaturesConfig>
    {
        public SignatureComponent[] Components { get; set; }

        public SignaturesConfig()
        {
            Components = new SignatureComponent[] { new SignatureComponent() };
        }
    }
}
