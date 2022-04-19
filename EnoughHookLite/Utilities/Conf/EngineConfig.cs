using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class EngineConfig : SerializableConf<EngineConfig>
    {
        [JsonProperty("process_name")]
        public string ProcessName { get; private set; }
        [JsonProperty("signatures")]
        public SignatureComponent[] Components { get; private set; }

        public EngineConfig()
        {
            Components = new SignatureComponent[] { new SignatureComponent() };
            ProcessName = "";
        }
    }
}
