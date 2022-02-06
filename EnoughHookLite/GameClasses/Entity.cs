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
        internal App App;
        public int Index { get; internal set; }
        public int Pointer { get; internal set; }

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

        public Vector3 VecOrigin { get { return App.Client.NativeModule.ReadStruct<Vector3>(Pointer + App.OffsetLoader.Offsets.Netvars.m_vecOrigin); } }
        public bool Dormant { get { return App.Client.NativeModule.ReadStruct<bool>(Pointer + App.OffsetLoader.Offsets.Signatures.m_bDormant); } }
        public float SpawnTime { get { return App.Client.NativeModule.ReadFloat(Pointer + App.OffsetLoader.Offsets.Signatures.m_flSpawnTime); } }

        public override string ToString()
        {
            return $"id:{Index} ptr:{Pointer} isnull:{IsNull}";
        }
    }
}
