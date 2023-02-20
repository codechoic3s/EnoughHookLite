using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration.Apis
{
    public sealed class NumericsAPI : SharedAPI
    {
        protected override void OnSetupModule(ScriptModule module)
        {
            
        }

        protected override void OnSetupTypes(ISharedGlobalHandler handler)
        {
            handler.AddType("Vector3", typeof(Vector3));
            handler.AddType("Vector2", typeof(Vector2));
            handler.AddType("Vector4", typeof(Vector4));
            handler.AddType("Matrix3x2", typeof(Matrix3x2));
            handler.AddType("Matrix4x4", typeof(Matrix4x4));
        }
    }
}
