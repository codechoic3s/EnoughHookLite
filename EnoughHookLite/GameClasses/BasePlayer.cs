using EnoughHookLite.Pointing;
using EnoughHookLite.Pointing.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class BasePlayer : Entity
    {
        private const string ClassName = "DT_BasePlayer";

        public BasePlayer(SubAPI api, int ptr, int index) : base(api, ptr, index)
        {
        }

        [Netvar(ClassName + ".m_fFlags")]
        private PointerCached pFlags;

        [Netvar(ClassName + ".m_iHealth")]
        private PointerCached pHealth;

        public int FFlags { get { return SubAPI.Process.RemoteMemory.ReadInt(Pointer + pFlags.Pointer); } }
        public int Health { get { return SubAPI.Process.RemoteMemory.ReadInt(Pointer + pHealth.Pointer); } }
    }
}
