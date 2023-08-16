using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class AModules : SerializableConf<AModules>
    {
        [JsonPropertyName("client_module")]
        public string ClientModule { get; private set; }
        [JsonPropertyName("engine_module")]
        public string EngineModule { get; private set; }

        [JsonPropertyName("custom_modules")]
        public List<string> CustomModules { get; private set; }

        public AModules()
        {
            ClientModule = "";
            EngineModule = "";
            CustomModules = new List<string>();
        }
    }
}
