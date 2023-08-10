using EnoughHookLite.Logging;
using EnoughHookLite.Modules;
using EnoughHookLite.Sys;
using EnoughHookLite.Utilities.Conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public sealed class SignatureManager
    {
        public SignatureDumper SignatureDumper { get; private set; }
        internal Dictionary<string, Signature> SignatureList;

        private SubAPI SubAPI;

        private LogEntry LogSigMgr;
        public SignatureManager(SubAPI api)
        {
            SubAPI = api;
            SignatureDumper = new SignatureDumper();
            SignatureList = new Dictionary<string, Signature>();

            LogSigMgr = new LogEntry(() => { return $"[SignatureManager] "; });
            App.LogHandler.AddEntry($"SignatureManager", LogSigMgr);
        }

        public void LoadSignatures(EngineConfig sc)
        {
            foreach (var item in SignatureList)
            {
                SignatureDumper.AddSignature(item.Value);
            }
            if (sc.ParallelSignatureScans) 
                SignatureDumper.ScanSignaturesParallel(sc.LoggingSignatureScans);
            else
                SignatureDumper.ScanSignatures(sc.LoggingSignatureScans);
        }

        public void ParseFromConfig(EngineConfig sc)
        {
            var components = sc.Components;
            var cco = components.LongLength;

            for (long i = 0; i < cco; i++)
            {
                var component = components[i];

                if (!SubAPI.TryGetModule(component.Module, out ManagedModule module))
                {
                    LogSigMgr.Log($"Failed get module {component.Module} by signature {component.Name}");
                    continue;
                }

                Signature signature = new Signature(module.NativeModule, component.Offsets, component.Extra, component.Relative, component.Name, component.Id, component.Signature);

                SignatureList.Add(component.Name, signature);
            }
        }
    }
}