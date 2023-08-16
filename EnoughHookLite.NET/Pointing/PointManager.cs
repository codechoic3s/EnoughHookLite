using EnoughHookLite.Logging;
using EnoughHookLite.Sys;
using EnoughHookLite.Utilities;
using EnoughHookLite.Utilities.ClientClassManaging;
using EnoughHookLite.Utilities.Conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing
{
    public sealed class PointManager
    {
        public SignatureManager SignatureManager { get; private set; }
        public ClientClassParser ClientClassParser { get; private set; }

        private Dictionary<ulong, PointerCached> Signatures;
        private Dictionary<string, PointerCached> Netvars;

        private LogEntry LogPM;
        private SubAPI SubAPI;
        public PointManager(SubAPI subAPI)
        {
            Signatures = new Dictionary<ulong, PointerCached>();
            Netvars = new Dictionary<string, PointerCached>();
            SubAPI = subAPI;

            LogPM = new LogEntry(() => { return "[PointManager] "; });
            App.LogHandler.AddEntry("PointManager", LogPM);
        }

        public bool AllocateSignature(SignaturesConsts id, out PointerCached pc)
        {
            return Signatures.TryGetValue((ulong)id, out pc);
        }
        public bool AllocateSignature(ulong id, out PointerCached pc)
        {
            return Signatures.TryGetValue(id, out pc);
        }
        public bool AllocateSpecialNetvar(string id, out PointerCached pc)
        {
            if (!Netvars.TryGetValue(id, out pc))
            {
                string[] splited = id.Split('.');

                var datatable = ClientClassParser.DataTables[splited[0]];
                var variable = datatable.GetProperty(splited[1]);
                pc = new PointerCached((uint)variable.Offset);
                Netvars.Add(id, pc);
                return true;
            }
            return true;
        }
        public bool AllocateNetvar(string id, out PointerCached pc)
        {
            if (!Netvars.TryGetValue(id, out pc))
            {
                string[] splited = id.Split('.');

                var datatable = ClientClassParser.DataTables[splited[0]];
                var variable = datatable.GetProperty(splited[1]);
                pc = new PointerCached((uint)variable.Offset);
                Netvars.Add(id, pc);
                return true;
            }
            return true;
        }

        public bool InitClientClasses()
        {
            LogPM.Log("Initing ClientClasses...");
            ClientClassParser = new ClientClassParser(SubAPI);
            if (!SubAPI.TypesParser.TryParse(ClientClassParser))
            {
                LogPM.Log("Failed parse ClientClass for signatures.");
                return false;
            }
            ClientClassParser.Parsing();
            return true;
        }
        public void InitSignatures(EngineConfig sc)
        {
            LogPM.Log("Initing signatures...");
            SignatureManager = new SignatureManager(SubAPI);
            SignatureManager.ParseFromConfig(sc);
            SignatureManager.LoadSignatures(sc);
            CacheParsedSignatures();
        }

        private void CacheParsedSignatures()
        {
            var slist = SignatureManager.SignatureDumper.Modules;
            foreach (var module in slist)
            {
                foreach (var sig in module.Value)
                {
                    if (sig.Finded && !Signatures.ContainsKey(sig.Id))
                    {
                        Signatures.Add(sig.Id, new PointerCached((uint)sig.Pointer));
                    }
                }
            }
            slist.Clear();
        }
    }
}
