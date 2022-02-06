using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Offsets
{
    public sealed class csgo
    {
        [JsonProperty("netvars")]
        public Netvars Netvars;

        [JsonProperty("signatures")]
        public Signatures Signatures;
    }
}
