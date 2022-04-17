using EnoughHookLite.Scripting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class MainConfig : SerializableConf<MainConfig>
    {
        [JsonProperty("script_fulldebug")]
        public bool ScriptFullDebug;

        [JsonProperty("script_autoreload")]
        public bool ScriptAutoReload;

        [JsonProperty("script_configs")]
        public Dictionary<string, ScriptConfig> JSConfigs;
        public MainConfig()
        {
            JSConfigs = new Dictionary<string, ScriptConfig>();
            ScriptFullDebug = false;
            ScriptAutoReload = false;
        }
    }
}
