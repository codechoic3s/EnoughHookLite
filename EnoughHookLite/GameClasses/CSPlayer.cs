using EnoughHookLite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class CSPlayer : Entity
    {
        public CSPlayer(App app, int index) : base(app, index)
        {
            /*
            _StudioBones = new mstudiobone_t[128];
            _StudioHitBoxes = new mstudiobbox_t[128];
            */
        }

        public int FFlags { get { return App.Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_fFlags); } }
        public Team Team { get { return (Team)App.Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_iTeamNum); } }
        public int Armor { get { return App.Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_ArmorValue); } }
        public int Health { get { return App.Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_iHealth); } }
        public bool HasHelmet { get { return App.Client.NativeModule.ReadStruct<bool>(Pointer + App.OffsetLoader.Offsets.Netvars.m_bHasHelmet); } }
        public bool HasDefuseKit { get { return App.Client.NativeModule.ReadStruct<bool>(Pointer + App.OffsetLoader.Offsets.Netvars.m_bHasDefuser); } }
        public bool IsDefusing { get { return App.Client.NativeModule.ReadStruct<bool>(Pointer + App.OffsetLoader.Offsets.Netvars.m_bIsDefusing); } }
        public bool InReload { get { return App.Client.NativeModule.ReadStruct<bool>(Pointer + App.OffsetLoader.Offsets.Netvars.m_bInReload); } }
        public bool Spotted { get { return App.Client.NativeModule.ReadStruct<bool>(Pointer + App.OffsetLoader.Offsets.Netvars.m_bSpotted); } }
        public bool SpottedByMask { get { return App.Client.NativeModule.ReadStruct<bool>(Pointer + App.OffsetLoader.Offsets.Netvars.m_bSpottedByMask); } }
        public bool IsScoped { get { return App.Client.NativeModule.ReadStruct<bool>(Pointer + App.OffsetLoader.Offsets.Netvars.m_bIsScoped); } }
        public int Clip1 { get { return App.Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_iClip1); } }

        public Rank Rank { get { return App.Client.PlayerResource.GetRank(Index); } }
        public int Wins { get { return App.Client.PlayerResource.GetWins(Index); } }

        public bool IsPlayer { get { var tm = (int)Team; bool ok = ((tm > 0) && (tm < 5)); return ok; } }
        public bool IsAlive { get { return Health > 0; } }

        public int BoneMatrixPointer { get { return App.Client.NativeModule.ReadInt(Pointer + App.OffsetLoader.Offsets.Netvars.m_dwBoneMatrix); } }

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

        public Vector3 GetBonePosition(int BoneID)
        {
            int bonematrix = BoneMatrixPointer;
            var adr = bonematrix + 0x30 * BoneID;
            float x = App.Client.NativeModule.ReadFloat(adr + 0x0C);
            float y = App.Client.NativeModule.ReadFloat(adr + 0x1C);
            float z = App.Client.NativeModule.ReadFloat(adr + 0x2C);
            return new Vector3(x, y, z);
        }
    }
}
