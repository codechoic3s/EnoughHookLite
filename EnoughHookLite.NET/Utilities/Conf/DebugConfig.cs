using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class DebugConfig : SerializableConf<DebugConfig>
    {
        [JsonPropertyName("dump_clientclasses_cpp")]
        public bool DumpClientClassesCPP;

        [JsonPropertyName("script_fulldebug")]
        public bool ScriptFullDebug;

        [JsonPropertyName("script_autoreload")]
        public bool ScriptAutoReload;

        [JsonPropertyName("protected_start")]
        public bool ProtectedStart;

        public DebugConfig()
        {
            DumpClientClassesCPP = false;
            ScriptFullDebug = false;
            ScriptAutoReload = false;
#if DEBUG
            ProtectedStart = false;
#else
            ProtectedStart = true;
#endif
        }
    }
}
