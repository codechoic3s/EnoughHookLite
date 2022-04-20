using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Apis
{
    public sealed class NumericsAPI : SharedAPI
    {
        public override void OnSetupAPI(ISharedHandler local)
        {
            local.AddType("Vector3", typeof(Vector3));
            local.AddType("Vector2", typeof(Vector2));
            local.AddType("Vector4", typeof(Vector4));
        }
    }
}
