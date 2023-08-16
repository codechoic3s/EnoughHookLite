using EnoughHookLite.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class MainConfig : SerializableConf<MainConfig>
    {
        [JsonPropertyName("script_configs")]
        public Dictionary<string, ScriptConfig> JSConfigs;
        public MainConfig()
        {
            JSConfigs = new Dictionary<string, ScriptConfig>();
        }
    }
}
