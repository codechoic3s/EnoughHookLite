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
            _StudioBones = new mstudiobone_t[128];
            _StudioHitBoxes = new mstudiobbox_t[128];
        }

        public int FFlags { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_fFlags); } }
        public Team Team { get { return (Team)App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_iTeamNum); } }
        public int Armor { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_ArmorValue); } }
        public int Health { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_iHealth); } }
        public bool HasHelmet { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bHasHelmet); } }
        public bool HasDefuseKit { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bHasDefuser); } }
        public bool IsDefusing { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bIsDefusing); } }
        public bool InReload { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bInReload); } }
        public bool Spotted { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bSpotted); } }
        public bool SpottedByMask { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bSpottedByMask); } }
        public bool IsScoped { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.netvars.m_bIsScoped); } }
        public int Clip1 { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_iClip1); } }

        public Rank Rank { get { return App.Client.PlayerResource.GetRank(Index); } }
        public int Wins { get { return App.Client.PlayerResource.GetWins(Index); } }

        public bool IsPlayer { get { var tm = (int)Team; bool ok = ((tm > 0) && (tm < 5)); return ok; } }
        public bool IsAlive { get { return Health > 0; } }

        public int BoneMatrixPointer { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_dwBoneMatrix); } }

        public int AddressStudioHdr
        {
            get
            {
                var addressToAddressStudioHdr = App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.signatures.m_pStudioHdr);
                return App.Client.ClientModule.ReadInt(addressToAddressStudioHdr); // deref
            }
        }

        /// <inheritdoc cref="studiohdr_t"/>
        public studiohdr_t StudioHdr
        {
            get
            {
                return App.Client.ClientModule.ReadStruct<studiohdr_t>(AddressStudioHdr);
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
                return App.Client.ClientModule.ReadStruct<mstudiohitboxset_t>(AddressHitBoxSet);
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
                    _StudioHitBoxes[i] = App.Client.ClientModule.ReadStruct<mstudiobbox_t>(AddressHitBoxSet + StudioHitBoxSet.hitboxindex + i * Marshal.SizeOf<mstudiobbox_t>());
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
                    _StudioBones[i] = App.Client.ClientModule.ReadStruct<mstudiobone_t>(AddressStudioHdr + StudioHdr.boneindex + i * Marshal.SizeOf<mstudiobone_t>());
                }
                return _StudioBones;
            }
        }

        private Matrix[] _BonesMatrices;
        public Matrix[] BonesMatrices
        {
            get
            {
                for (var boneId = 0; boneId < BonesPos.Length; boneId++)
                {
                    var matrix = App.Client.ClientModule.ReadStruct<matrix3x4_t>(BoneMatrixPointer + boneId * Marshal.SizeOf<matrix3x4_t>());
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
                for (var boneId = 0; boneId < BonesPos.Length; boneId++)
                {
                    var matrix = App.Client.ClientModule.ReadStruct<matrix3x4_t>(BoneMatrixPointer + boneId * Marshal.SizeOf<matrix3x4_t>());
                    _BonesPos[boneId] = new Vector3(matrix.m30, matrix.m31, matrix.m32);
                }
                return _BonesPos;
            }
        }

        public Vector3 GetBonePosition(int BoneID)
        {
            int bonematrix = BoneMatrixPointer;
            float x = App.Client.ClientModule.ReadFloat(bonematrix + 0x30 * BoneID + 0x0C);
            float y = App.Client.ClientModule.ReadFloat(bonematrix + 0x30 * BoneID + 0x1C);
            float z = App.Client.ClientModule.ReadFloat(bonematrix + 0x30 * BoneID + 0x2C);
            return new Vector3(x, y, z);
        }
    }
}
