using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class EngineConfig : SerializableConf<EngineConfig>
    {
        [JsonPropertyName("process_name")]
        public string ProcessName { get; private set; }
        [JsonPropertyName("loggingSignatureScans")]
        public bool LoggingSignatureScans { get; private set; }
        [JsonPropertyName("parallelSignatureScans")]
        public bool ParallelSignatureScans { get; private set; }
        [JsonPropertyName("signatures")]
        public SignatureComponent[] Components { get; private set; }

        public EngineConfig()
        {
            Components = new SignatureComponent[] { new SignatureComponent() };
            ProcessName = "";
            LoggingSignatureScans = true;
            ParallelSignatureScans = true;
        }
    }
}
