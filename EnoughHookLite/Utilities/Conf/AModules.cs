using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class AModules : SerializableConf<AModules>
    {
        [JsonProperty("client_module")]
        public string ClientModule { get; private set; }
        [JsonProperty("engine_module")]
        public string EngineModule { get; private set; }

        [JsonProperty("custom_modules")]
        public List<string> CustomModules { get; private set; }

        public AModules()
        {
            ClientModule = "";
            EngineModule = "";
            CustomModules = new List<string>();
        }
    }
}
