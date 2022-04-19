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
using System.IO;

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
        private PointerCached pGetAllClasses;
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

        public void DumpCppClasses(string file)
        {
            var tables = DataTables.Values.OrderBy(x => x.BaseClassDepth.Value);
            using (StreamWriter writer = new StreamWriter(file, false))
                foreach (var table in tables)
                    DumpCppClass(writer, table);
        }
        private void DumpCppClass(TextWriter o, ManagedRecvTable table)
        {
            const int padding = 32;

            var bc = table.BaseClass.Value;
            int currentOffset = 0;
            int pad = 0;

            if (bc != null)
                currentOffset = bc.Size.Value;

            var props = table.RecvProps.Value.OrderBy(x => x.Offset).ToArray();

            if (bc != null)
                o.WriteLine("class {0} : {1} {{", table.NetTableName.Value, bc.NetTableName.Value);
            else
                o.WriteLine("class {0} {{", table.NetTableName.Value);

            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                if (prop.VarName.Value == "baseclass")
                    continue;

                //Pad
                if (prop.Offset != currentOffset)
                {
                    o.WriteLine("\t{0} {1}//0x{2}",
                        "char".PadRight(padding, ' '),
                        string.Format("pad{0}[{1}];",
                            (pad++).ToString().PadLeft(2, '0'),
                            prop.Offset - currentOffset
                        ).PadRight(padding, ' '),
                        currentOffset.ToString("X8"));
                    currentOffset = prop.Offset;
                }

                switch (prop.PropType)
                {
                    case ePropType.DataTable:
                        o.WriteLine("\t{0} {1}//0x{2}",
                            prop.SubTable.Value.NetTableName.Value.PadRight(padding, ' '),
                            (prop.VarName.Value + ";").PadRight(padding, ' '),
                            prop.Offset.ToString("X8"));
                        break;
                    case ePropType.Array:
                        o.WriteLine("\t{0} {1}//0x{2}",
                            (prop.ArrayProp.Value.Length > 0 ? prop.ArrayProp.Value[0].PropType.ToString() : "void*").PadRight(padding, ' '),
                            string.Format("{0}[{1}];",
                                prop.VarName.Value,
                                prop.ElementCount).PadRight(padding, ' '),
                            prop.Offset.ToString("X8"));
                        break;
                    default:
                        o.WriteLine("\t{0} {1}//0x{2}",
                            prop.PropType.ToString().PadRight(padding, ' '),
                            (prop.VarName.Value + ";").PadRight(padding, ' '),
                            prop.Offset.ToString("X8"));
                        break;
                }
                if (i < props.Length - 1)
                    currentOffset += System.Math.Min(prop.Size.Value, props[i + 1].Offset - currentOffset);
                else
                    currentOffset += prop.Size.Value;
            }

            o.WriteLine("}\n");
        }
    }
}
