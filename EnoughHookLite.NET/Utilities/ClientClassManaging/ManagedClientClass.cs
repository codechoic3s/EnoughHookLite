using EnoughHookLite.OtherCode.Structs;
using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.ClientClassManaging
{
    public sealed class ManagedClientClass : IUnmanagedObject
    {
        public bool Computed { get; private set; }

        public uint Pointer { get; private set; }

        public CompileCache<ClientClass> ClientClass { get; private set; }
        public CompileCache<string> NetworkName { get; private set; }
        public int ClassID => ClientClass.Value.ClassID;
        public ManagedRecvTable RecvTable { get; private set; }

        private RemoteMemory RemoteMemory;
        public ManagedClientClass(RemoteMemory rm) { RemoteMemory = rm; }

        public void Compute(uint ptr)
        {
            Pointer = ptr;

            ClientClass = new CompileCache<ClientClass>(() => { return RemoteMemory.ReadStruct<ClientClass>(ptr); });
            NetworkName = new CompileCache<string>(() => { return RemoteMemory.ReadString(ClientClass.Value.pNetworkName, 32, Encoding.ASCII); });

            uint precvtable = ClientClass.Value.pRecvTable;
            if (precvtable == 0xffff || precvtable == -1 || precvtable == 0)
                RecvTable = null;
            else
            {
                RecvTable = new ManagedRecvTable(RemoteMemory);
                RecvTable.Compute(precvtable);
            }
            Computed = true;
        }
    }
}
