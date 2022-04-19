using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class DebugConfig : SerializableConf<DebugConfig>
    {
        [JsonProperty("dump_clientclasses_cpp")]
        public bool DumpClientClassesCPP { get; private set; }

        [JsonProperty("script_fulldebug")]
        public bool ScriptFullDebug { get; private set; }

        [JsonProperty("script_autoreload")]
        public bool ScriptAutoReload { get; private set; }

        public DebugConfig()
        {
            DumpClientClassesCPP = false;
            ScriptFullDebug = false;
            ScriptAutoReload = false;
        }
    }
}
