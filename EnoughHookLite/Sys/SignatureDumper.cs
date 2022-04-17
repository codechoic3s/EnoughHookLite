using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Sys
{
    public sealed class SignatureDumper
    {
        private Dictionary<Module, List<Signature>> Modules;

        public SignatureDumper()
        {
            Modules = new Dictionary<Module, List<Signature>>();
        }

        public void AddSignature(Signature sig)
        {
            Module m = sig.Module;
            if (Modules.TryGetValue(m, out List<Signature> list))
            {
                list.Add(sig);
            }
            else
            {
                list = new List<Signature>
                {
                    sig
                };
                Modules.Add(m, list);
            }
        }
        public void AddSignature(Module m, params short[] sig)
        {
            AddSignature((m, sig));
        }

        public void ScanSignatures(bool logging = false)
        {
            if (logging)
                LogIt("Started scanning...");
            ulong scanned = 0;
            foreach (var item in Modules)
            {
                var module = item.Key;
                var list = item.Value;
                var lco = list.Count;

                var mco = module.Size;
                var memorymodule = module.Process.ReadData(module.BaseAdr, mco);

                bool finded = false;

                for (var i = 0; i < mco; i++)
                {
                    if (finded)
                        break;
                    for (var o = 0; o < lco; o++)
                    {
                        if (finded)
                            break;
                        var tsig = list[o];
                        var sig = tsig.Sig;
                        var sco = sig.Length;

                        if (memorymodule[i] == sig[0] || sig[0] == -1)
                        {
                            bool ok = true;
                            for (var p = 1; p < sco; p++)
                            {
                                if (memorymodule[i + p] != sig[p])
                                {
                                    ok = false;
                                    break;
                                }
                            }

                            if (ok)
                            {
                                int result = module.BaseAdr + i;

                                var oco = tsig.Offsets.Length;
                                for (var u = 0; u < oco; u++)
                                {
                                    result += tsig.Offsets[u];
                                    result = module.Process.RemoteMemory.ReadInt(result);
                                }

                                result += tsig.Extra - module.BaseAdr;
                                result += tsig.Relative ? 0 : module.BaseAdr;

                                tsig.Pointer = result;
                                tsig.Finded = true;
                                finded = true;
                                scanned++;
                            }
                        }
                    }
                }
            }

            Modules.Clear(); // clear all

            if (logging)
                LogIt($"Ended scanning with {scanned} signatures.");
        }

        private void LogIt(string log)
        {
            App.Log.LogIt("[SignatureDumper] " + log);
        }
    }
}
