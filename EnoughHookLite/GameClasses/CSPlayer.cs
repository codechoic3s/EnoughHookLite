using EnoughHookLite.OtherCode.Structs;
using EnoughHookLite.Pointing;
using EnoughHookLite.Pointing.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class CSPlayer : BasePlayer
    {
        private const string ClassName = "DT_CSPlayer";
        public CSPlayer(SubAPI api, int ptr, int index) : base(api, ptr, index)
        {
            /*
            _StudioBones = new mstudiobone_t[128];
            _StudioHitBoxes = new mstudiobbox_t[128];
            */
        }

        /*
        [Netvar(ClassName + ".m_iTeamNum")]
        private PointerCached pTeamNum;
        [Netvar(ClassName + ".m_ArmorValue")]
        private PointerCached pArmorValue;
        [Netvar(ClassName + ".m_bHasHelmet")]
        private PointerCached pHasHelmet;
        [Netvar(ClassName + ".m_bHasDefuser")]
        private PointerCached pHasDefuser;
        [Netvar(ClassName + ".m_bIsDefusing")]
        private PointerCached pIsDefusing;
        [Netvar(ClassName + ".m_bIsScoped")]
        private PointerCached pIsScoped;
        [Netvar(ClassName + ".m_dwBoneMatrix")]
        private SpecialPointerCached pBoneMatrix;
        
        public Team Team { get { return (Team)SubAPI.Process.RemoteMemory.ReadInt(Pointer + pTeamNum.Pointer); } }
        public int Armor { get { return SubAPI.Process.RemoteMemory.ReadInt(Pointer + pArmorValue.Pointer); } }
        
        public bool HasHelmet { get { return SubAPI.Process.RemoteMemory.ReadStruct<bool>(Pointer + pHasHelmet.Pointer); } }
        public bool HasDefuseKit { get { return SubAPI.Process.RemoteMemory.ReadStruct<bool>(Pointer + pHasDefuser.Pointer); } }
        public bool IsDefusing { get { return SubAPI.Process.RemoteMemory.ReadStruct<bool>(Pointer + pIsDefusing.Pointer); } }
        public bool IsScoped { get { return SubAPI.Process.RemoteMemory.ReadStruct<bool>(Pointer + pIsScoped.Pointer); } }

        

        public bool IsPlayer { get { var tm = (int)Team; bool ok = ((tm > 0) && (tm < 5)); return ok; } }
        public bool IsAlive { get { return Health > 0; } }

        public int BoneMatrixPointer { get { return SubAPI.Process.RemoteMemory.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_dwBoneMatrix); } }
        */
        public Rank Rank { get { return SubAPI.Client.PlayerResource.GetRank(Index); } }
        public int Wins { get { return SubAPI.Client.PlayerResource.GetWins(Index); } }
        /*
        public int AddressStudioHdr
        {
            get
            {
                var addressToAddressStudioHdr = App.Client.NativeModule.ReadInt(Pointer + Offsets.App.OffsetLoader.Offsets.Signatures.m_pStudioHdr);
                return App.Client.NativeModule.ReadInt(addressToAddressStudioHdr); // deref
            }
        }

        /// <inheritdoc cref="studiohdr_t"/>
        public studiohdr_t StudioHdr
        {
            get
            {
                return App.Client.NativeModule.ReadStruct<studiohdr_t>(AddressStudioHdr);
            }
        }

        public int AddressHitBoxSet
        {
            get
            {
                return AddressStudioHdr + StudioHdr.hitboxsetindex;
            }
        }

        /// <inheritdoc cref="mstudiohitboxset_t"/>
        public mstudiohitboxset_t StudioHitBoxSet
        {
            get
            {
                return App.Client.NativeModule.ReadStruct<mstudiohitboxset_t>(AddressHitBoxSet);
            }
        }

        private mstudiobbox_t[] _StudioHitBoxes;
        public mstudiobbox_t[] StudioHitBoxes
        {
            get
            {
                // read
                for (var i = 0; i < StudioHitBoxSet.numhitboxes; i++)
                {
                    var adr = AddressHitBoxSet + StudioHitBoxSet.hitboxindex + i * Marshal.SizeOf<mstudiobbox_t>();
                    if (adr < 0)
                    {

                    }
                    _StudioHitBoxes[i] = App.Client.NativeModule.ReadStruct<mstudiobbox_t>(adr);
                }
                return _StudioHitBoxes;
            }
        }

        private mstudiobone_t[] _StudioBones;
        public mstudiobone_t[] StudioBones
        {
            get
            {
                for (var i = 0; i < StudioHdr.numbones; i++)
                {
                    _StudioBones[i] = App.Client.NativeModule.ReadStruct<mstudiobone_t>((uint)(AddressStudioHdr + StudioHdr.boneindex + i * Marshal.SizeOf<mstudiobone_t>()));
                }
                return _StudioBones;
            }
        }

        private Matrix[] _BonesMatrices;
        public Matrix[] BonesMatrices
        {
            get
            {
                var bonematrix = BoneMatrixPointer;
                for (var boneId = 0; boneId < BonesPos.Length; boneId++)
                {
                    var matrix = App.Client.NativeModule.ReadStruct<matrix3x4_t>(bonematrix + boneId * Marshal.SizeOf<matrix3x4_t>());
                    _BonesMatrices[boneId] = matrix.ToMatrix();
                }
                return _BonesMatrices;
            }
        }

        private Vector3[] _BonesPos;
        public Vector3[] BonesPos
        {
            get
            {
                var bonematrix = BoneMatrixPointer;
                for (var boneId = 0; boneId < BonesPos.Length; boneId++)
                {
                    var matrix = App.Client.NativeModule.ReadStruct<matrix3x4_t>(bonematrix + boneId * Marshal.SizeOf<matrix3x4_t>());
                    _BonesPos[boneId] = new Vector3(matrix.m30, matrix.m31, matrix.m32);
                }
                return _BonesPos;
            }
        }
        */
        /*
        public Vector3 GetBonePosition(int BoneID)
        {
            int bonematrix = BoneMatrixPointer;
            int adr = bonematrix + 0x30 * BoneID;
            BoneMartixVector vector = SubAPI.Process.RemoteMemory.ReadStruct<BoneMartixVector>(adr);
            //float x = SubAPI.Client.NativeModule.Process.RemoteMemory.ReadFloat(adr + 0x0C);
            //float y = SubAPI.Client.NativeModule.Process.RemoteMemory.ReadFloat(adr + 0x1C);
            //float z = SubAPI.Client.NativeModule.Process.RemoteMemory.ReadFloat(adr + 0x2C);
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
        */
    }
}
