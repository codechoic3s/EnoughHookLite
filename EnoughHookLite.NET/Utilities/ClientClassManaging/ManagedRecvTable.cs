using EnoughHookLite.OtherCode.Structs;
using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.ClientClassManaging
{
    public sealed class ManagedRecvTable : IUnmanagedObject
    {
        public uint Pointer { get; private set; }
        public bool Computed { get; private set; }

        public CompileCache<RecvTable_t> RecvTable { get; private set; }
        public CompileCache<string> NetTableName { get; private set; }
        public CompileCache<ManagedRecvProp[]> RecvProps { get; private set; }
        public CompileCache<uint> HighestOffset { get; private set; }
        public CompileCache<int> BaseClassDepth { get; private set; }
        public CompileCache<ManagedRecvTable> BaseClass { get; private set; }
        //public RemoteObject<ManagedRecvTable[]> BaseClasses { get; private set; }
        public CompileCache<uint> Size { get; private set; }

        private RemoteMemory RemoteMemory;
        public ManagedRecvTable(RemoteMemory rm) { RemoteMemory = rm; }

        public void Compute(uint pointer)
        {
            RecvTable = new CompileCache<RecvTable_t>(() => RemoteMemory.ReadStruct<RecvTable_t>(pointer));
            NetTableName = new CompileCache<string>(() => RemoteMemory.ReadString(RecvTable.Value.pNetTableName, 32, Encoding.ASCII));
            RecvProps = new CompileCache<ManagedRecvProp[]>(() =>
            {
                RecvTable_t recvtable = RecvTable.Value;
                uint npco = recvtable.nProps;
                ManagedRecvProp[] props = new ManagedRecvProp[npco];
                for (uint i = 0; i < npco; i++)
                {
                    ManagedRecvProp prop = new ManagedRecvProp(RemoteMemory, this);
                    prop.Compute(recvtable.pProps + i * 0x3C);
                    props[i] = prop;
                }
                return props;
            });
            
            HighestOffset = new CompileCache<uint>(() => 
            {
                ManagedRecvProp[] recvprops = RecvProps.Value;
                return recvprops.Length == 0 ? 0 : recvprops.Max(x => x == null ? 0 : x.Offset);
            });

            BaseClass = new CompileCache<ManagedRecvTable>(() => 
            {
                ManagedRecvProp[] recvprops = RecvProps.Value;
                if (recvprops.Any(x => x.VarName.Value == "baseclass"))
                    return recvprops.First(x => x.VarName.Value == "baseclass").SubTable.Value;
                return null;
            });

            BaseClassDepth = new CompileCache<int>(() => 
            {
                ManagedRecvTable bc = BaseClass.Value;
                return bc is null ? 0 : 1 + bc.BaseClassDepth.Value;
            });

            Size = new CompileCache<uint>(() =>
            {
                ManagedRecvProp[] recvprops = RecvProps.Value;
                return recvprops.Length > 0 ? recvprops.Last().Size.Value : 0;
            });

            Computed = true;
        }

        public ManagedRecvProp GetProperty(string propertyName)
        {
            return RecvProps.Value.First(x => x.VarName.Value == propertyName);
        }
        public ManagedRecvProp this[string propertyName]
        {
            get
            {
                return RecvProps.Value.First(x => x.VarName.Value == propertyName);
            }
        }
    }
}
