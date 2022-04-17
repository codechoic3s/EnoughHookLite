using EnoughHookLite.Sys;
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
    }
}
