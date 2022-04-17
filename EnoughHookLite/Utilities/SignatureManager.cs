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

        public SignatureManager(SubAPI api)
        {
            SubAPI = api;
            SignatureDumper = new SignatureDumper();
            SignatureList = new Dictionary<string, Signature>();
        }

        public void LoadSignatures()
        {
            foreach (var item in SignatureList)
            {
                SignatureDumper.AddSignature(item.Value);
            }
            SignatureDumper.ScanSignatures(true);
        }

        private void LogIt(string log)
        {
            App.Log.LogIt("[SignatureManager] " + log);
        }

        public void ParseFromConfig(SignaturesConfig sc)
        {
            var components = sc.Components;
            var cco = components.LongLength;

            for (long i = 0; i < cco; i++)
            {
                var component = components[i];

                if (!SubAPI.TryGetCustomModule(component.Module, out ManagedModule module))
                {
                    LogIt($"Failed get module {component.Module} by signature {component.Name}");
                    continue;
                }

                Signature signature = new Signature(module.NativeModule, component.Offsets, component.Extra, component.Relative, component.Signature);

                SignatureList.Add(component.Name, signature);
            }
        }
    }
}
