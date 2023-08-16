using EnoughHookLite.Pointing;
using EnoughHookLite.Pointing.Attributes;
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
        public uint Pointer { get; internal set; }
        public int Index { get; internal set; }
        public ManagedClientClass ClientClass { get
            {
                uint vmtAddress = Pointer + 0x8;
                if (VmtToClassID.Instance.ContainsVMT(vmtAddress))
                    return VmtToClassID.Instance[vmtAddress];
                else
                {
                    var rm = SubAPI.Process.RemoteMemory;

                    int pFn = rm.ReadInt(vmtAddress + 2 * 0x04);
                    if ((long)pFn == 0xffffffffL || (long)pFn == 0L)
                        return null;

                    int address = rm.ReadInt(pFn + 1);
                    var clientClass = SubAPI.PointManager.ClientClassParser.ClientClasses.FirstOrDefault(x => x.Pointer == address);

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

        public Entity(SubAPI api, uint ptr, int index)
        {
            SubAPI = api;
            Pointer = ptr;
            Index = index;
        }

        [Signature(SignaturesConsts.m_bDormant)]
        private PointerCached pDormant;

        [Signature(SignaturesConsts.m_flSpawnTime)]
        private PointerCached pSpawnTime;

        [Netvar("DT_BaseEntity.m_vecOrigin")]
        private PointerCached pVecOrigin;

        public Vector3 VecOrigin { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadStruct<Vector3>(Pointer + pVecOrigin.Pointer); } }
        public bool Dormant { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadStruct<bool>(Pointer + pDormant.Pointer); } }
        public float SpawnTime { get { return SubAPI.Client.NativeModule.Process.RemoteMemory.ReadFloat(Pointer + pSpawnTime.Pointer); } }

        public override string ToString()
        {
            return $"ptr:{Pointer} isnull:{IsNull}";
        }
    }
}
