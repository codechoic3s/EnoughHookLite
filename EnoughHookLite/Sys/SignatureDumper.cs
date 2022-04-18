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
                LogIt($"Exists {m.Name} adds {sig.Name}");
            }
            else
            {
                list = new List<Signature>
                {
                    sig
                };
                Modules.Add(m, list);
                LogIt($"Creates {m.Name} adds {sig.Name}");
            }
        }

        public void ScanSignatures(bool logging = false)
        {
            if (logging)
                LogIt($"Started scanning {CountAllSignatures(Modules)}...");
            ulong scanned = 0;

            foreach (var item in Modules)
            {
                var module = item.Key;
                var list = item.Value;
                var lco = list.Count;

                var mco = module.Size;
                var memorymodule = module.Process.ReadData(module.BaseAdr, mco);

                for (var i = 0; i < mco; i++)
                {
                    for (var o = 0; o < lco; o++)
                    {
                        var tsig = list[o];
                        if (tsig.Finded)
                            continue;

                        var sig = tsig.Sig;
                        var sco = sig.Length;

                        if (memorymodule[i] == sig[0] || sig[0] == -1)
                        {
                            bool ok = true;
                            for (var p = 0; p < sco; p++)
                            {
                                var q1 = memorymodule[i + p] == sig[p] || sig[p] == -1;
                                if (!q1)
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
                                LogIt($"founded {tsig.Name} at {tsig.Pointer}");
                                scanned++;
                            }
                        }
                    }
                }
            }

            if (logging)
                LogIt($"Ended scanning with {scanned}/{CountAllSignatures(Modules)} signatures.");

            Modules.Clear(); // clear all
        }

        private ulong CountAllSignatures(Dictionary<Module, List<Signature>> modules)
        {
            ulong count = 0;
            foreach (var module in modules)
            {
                count += (ulong)module.Value.Count;
            }
            return count;
        }

        private void LogIt(string log)
        {
            App.Log.LogIt("[SignatureDumper] " + log);
        }
    }
}
