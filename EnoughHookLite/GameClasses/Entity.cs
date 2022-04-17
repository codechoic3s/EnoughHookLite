using EnoughHookLite.Utilities.ClientClassManaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class Entity
    {
        internal SubAPI SubAPI;
        public int Pointer { get; internal set; }
        public int Index { get; internal set; }
        public ManagedClientClass ClientClass { get
            {
                int vmtAddress = Pointer + 0x8;
                if (VmtToClassID.Instance.ContainsVMT(vmtAddress))
                    return VmtToClassID.Instance[vmtAddress];
                else
                {
                    var rm = SubAPI.Process.RemoteMemory;

                    int pFn = rm.ReadInt(vmtAddress + 2 * 0x04);
                    if ((long)pFn == 0xffffffffL || (long)pFn == 0L)
                        return null;

                    int address = rm.ReadInt(pFn + 1);
                    var clientClass = ClientClassParser.Instance.ClientClasses.FirstOrDefault(x => x.Pointer == address);

                    if (clientClass != null)
                        VmtToClassID.Instance[vmtAddress] = clientClass;

                    return clientClass;
                }
            } }

        public bool IsNull
        {
            get
            {
                return Pointer == 0;
            }
        }

        public Entity(SubAPI api, int ptr, int index)
        {
            SubAPI = api;
            Pointer = ptr;
            Index = index;
        }

        public Vector3 VecOrigin { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadStruct<Vector3>(Pointer + App.OffsetLoader.Offsets.Netvars.m_vecOrigin); } }
        public bool Dormant { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadStruct<bool>(Pointer + App.OffsetLoader.Offsets.Signatures.m_bDormant); } }
        public float SpawnTime { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadFloat(Pointer + App.OffsetLoader.Offsets.Signatures.m_flSpawnTime); } }

        public override string ToString()
        {
            return $"ptr:{Pointer} isnull:{IsNull}";
        }
    }
}
