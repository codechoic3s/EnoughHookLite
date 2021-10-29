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
        public App App;
        public int Index;
        public int Pointer;

        public bool IsNull
        {
            get
            {
                return Pointer == 0;
            }
        }

        public Entity(App app, int index)
        {
            App = app;
            Index = index;
        }

        public Vector3 VecOrigin { get { return App.Client.ClientModule.ReadStruct<Vector3>(Pointer + Offsets.csgo.netvars.m_vecOrigin); } }
        public bool Dormant { get { return App.Client.ClientModule.ReadStruct<bool>(Pointer + Offsets.csgo.signatures.m_bDormant); } }
        public float SpawnTime { get { return App.Client.ClientModule.ReadFloat(Pointer + Offsets.csgo.signatures.m_flSpawnTime); } }
    }
}
