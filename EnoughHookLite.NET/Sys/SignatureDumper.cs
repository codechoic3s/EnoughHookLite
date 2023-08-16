using EnoughHookLite.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Sys
{
    public sealed class SignatureDumper
    {
        internal Dictionary<Module, List<Signature>> Modules { get; private set; }
        
        private LogEntry LogSigDump;
        public SignatureDumper()
        {
            Modules = new Dictionary<Module, List<Signature>>();

            LogSigDump = new LogEntry(() => { return $"[SignatureDumper] "; });
            App.LogHandler.AddEntry($"SignatureDumper", LogSigDump);
        }

        public void AddSignature(Signature sig)
        {
            Module m = sig.Module;
            if (Modules.TryGetValue(m, out List<Signature> list))
            {
                list.Add(sig);
                //LogIt($"Exists {m.Name} adds {sig.Name}");
            }
            else
            {
                list = new List<Signature>
                {
                    sig
                };
                Modules.Add(m, list);
                //LogIt($"Creates {m.Name} adds {sig.Name}");
            }
        }
        public void ScanSignaturesParallel(bool logging = false)
        {
            if (logging)
                LogSigDump.Log($"Started scanning {CountAllSignatures(Modules)}...");
            ulong scanned = 0;

            //foreach (var item in Modules)
            Parallel.ForEach(Modules,
            (item) =>
            {
                var module = item.Key;
                var list = item.Value;
                var lco = list.Count;

                var mco = module.Size;
                var memorymodule = module.Process.ReadData(module.BaseAdr, mco);

                //for (uint i = 0; i < mco; i++)
                Parallel.For(0, lco,
                (o) =>
                {
                    var tsig = list[o];
                    var sig = tsig.Sig;
                    var sco = sig.Length;

                    for (uint i = 0; i < mco; i++)
                    {
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
                                uint result = module.BaseAdr + i;

                                var oco = tsig.Offsets.Length;
                                for (var u = 0; u < oco; u++)
                                //Parallel.For(0, oco,
                                //(u) =>
                                {
                                    var offset = tsig.Offsets[u];
                                    if (offset >= 0)
                                        result += (uint)offset;
                                    else if (offset < 0)
                                        result -= (uint)(offset - offset - offset);
                                    result = module.Process.RemoteMemory.ReadUInt(result);
                                }

                                result += tsig.Extra - module.BaseAdr;
                                result += tsig.Relative ? 0 : module.BaseAdr;

                                tsig.Pointer = result;
                                tsig.Finded = true;
                                LogSigDump.Log($"founded {tsig.Name} at {tsig.Pointer}");
                                scanned++;
                            }
                        }
                    }
                });
            });

            if (logging)
                LogSigDump.Log($"Ended scanning with {scanned}/{CountAllSignatures(Modules)} signatures.");
        }
        public void ScanSignatures(bool logging = false)
        {
            if (logging)
                LogSigDump.Log($"Started scanning {CountAllSignatures(Modules)}...");
            ulong scanned = 0;

            foreach (var item in Modules)
            {
                var module = item.Key;
                var list = item.Value;
                var lco = list.Count;

                var mco = module.Size;
                var memorymodule = module.Process.ReadData(module.BaseAdr, mco);

                for (var o = 0; o < lco; o++)
                {
                    var tsig = list[o];
                    if (tsig.Finded)
                        continue;

                    for (uint i = 0; i < mco; i++)
                    {
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
                                uint result = module.BaseAdr + i;

                                var oco = tsig.Offsets.Length;
                                for (var u = 0; u < oco; u++)
                                {
                                    var offset = tsig.Offsets[u];
                                    if (offset >= 0)
                                        result += (uint)offset;
                                    else if (offset < 0)
                                        result -= (uint)(offset - offset - offset);
                                    result = module.Process.RemoteMemory.ReadUInt(result);
                                }

                                result += tsig.Extra - module.BaseAdr;
                                result += tsig.Relative ? 0 : module.BaseAdr;

                                tsig.Pointer = result;
                                tsig.Finded = true;
                                LogSigDump.Log($"founded {tsig.Name} at {tsig.Pointer}");
                                scanned++;
                            }
                        }
                    }
                }
            }

            if (logging)
                LogSigDump.Log($"Ended scanning with {scanned}/{CountAllSignatures(Modules)} signatures.");
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
    }
}
