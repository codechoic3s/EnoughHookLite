using EnoughHookLite.Scripting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public sealed class EHLConfig : SerializableConf<EHLConfig>
    {
        [JsonProperty("script_fulldebug")]
        public bool ScriptFullDebug;

        [JsonProperty("script_autoreload")]
        public bool ScriptAutoReload;

        [JsonProperty("script_configs")]
        public Dictionary<string, JSConfig> JSConfigs;

        public EHLConfig()
        {
            JSConfigs = new Dictionary<string, JSConfig>();
            ScriptFullDebug = false;
            ScriptAutoReload = false;
        }
    }
}
