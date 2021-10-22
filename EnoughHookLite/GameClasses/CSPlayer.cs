using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class CSPlayer : Entity
    {
        public CSPlayer(App app, int index) : base(app, index)
        {
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

        public int BoneMatrixPointer { get { return App.Client.ClientModule.ReadInt(Pointer + Offsets.csgo.netvars.m_dwBoneMatrix); } }

        public Vector3 GetBonePosition(int BoneID)
        {
            int bonematrix = BoneMatrixPointer;
            float x = App.Client.ClientModule.ReadFloat(bonematrix + 0x30 * BoneID + 0x0C);
            float y = App.Client.ClientModule.ReadFloat(bonematrix + 0x30 * BoneID + 0x1C);
            float z = App.Client.ClientModule.ReadFloat(bonematrix + 0x30 * BoneID + 0x2C);
            //Console.WriteLine($"x:{x} y:{y} z:{z}");
            return new Vector3(x, y, z);
        }
    }
}
