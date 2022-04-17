using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnoughHookLite.OtherCode.Structs;
using System.Threading;
using System.Runtime.InteropServices;
using EnoughHookLite.Pointing;
using EnoughHookLite.Pointing.Attributes;
namespace EnoughHookLite.Utilities.ClientClassManaging
{
    public sealed class ClientClassParser
    {
        public ManagedClientClass[] ClientClasses { get; private set; }
        public Dictionary<string, ManagedRecvTable> DataTables { get; private set; }
        public SubAPI SubAPI { get; private set; }
        public ClientClassParser(SubAPI subapi)
        {
            SubAPI = subapi;
        }
        [Signature(SignaturesConsts.dwGetAllClasses)]
        public PointerCached pGetAllClasses;
        public void Parsing()
        {
            LogIt("Parsing client classes...");
            do
            {
                Parse();
                Thread.Sleep(500);
            } while (DataTables.Count == 0);
            LogIt("Successful parsed.");
        }
        private void LogIt(string log)
        {
            App.Log.LogIt("[ClientClassParser] " + log);
        }
        private void Parse()
        {
            List<ManagedClientClass> classes = new List<ManagedClientClass>();
            DataTables = new Dictionary<string, ManagedRecvTable>();
            RemoteMemory rm = SubAPI.Process.RemoteMemory;
            int pFirst = SubAPI.Client.NativeModule.BaseAdr + pGetAllClasses.Pointer;
            ClientClass cls = rm.ReadStruct<ClientClass>(pFirst);
            for (; cls.pNext != 0; pFirst = cls.pNext, cls = rm.ReadStruct<ClientClass>(pFirst))
            {
                ManagedClientClass mcc = new ManagedClientClass(rm);
                mcc.Compute(pFirst);
                classes.Add(mcc);
            }

            ClientClasses = classes.OrderBy(x => x.ClassID).ToArray();
            long cco = ClientClasses.LongLength;
            for (long i = 0; i < cco; i++)
            {
                ManagedClientClass _cls = ClientClasses[i];
                CrawlDataTables(_cls.RecvTable);
            }
        }

        private void CrawlDataTables(ManagedRecvTable table)
        {
            if (table == null)
                return;
            var pname = table.NetTableName.Value;
            if (!DataTables.ContainsKey(pname))
                DataTables[pname] = table;

            ManagedRecvProp[] recvprops = table.RecvProps.Value;
            long rpco = recvprops.LongLength;
            for (long i = 0; i < rpco; i++)
            {
                var prop = recvprops[i];
                if (prop.SubTable != null)
                    CrawlDataTables(prop.SubTable.Value);
            }
        }
    }
}
