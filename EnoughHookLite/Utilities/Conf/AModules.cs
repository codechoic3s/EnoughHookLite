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
        public string ClientModule;
        [JsonProperty("engine_module")]
        public string EngineModule;

        [JsonProperty("other_modules")]
        public List<string> OtherModules;

        public AModules()
        {
            ClientModule = "";
            EngineModule = "";
            OtherModules = new List<string>();
        }
    }
}
