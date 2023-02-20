using EnoughHookLite.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration.Apis
{
    public sealed class CameraAPI : SharedAPI
    {
        private Camera Camera;

        public CameraAPI(Camera cam)
        {
            Camera = cam;
        }

        protected override void OnSetupModule(ScriptModule module)
        {
            module.AddDelegate("WorldToScreen", (Func<Vector3, Vector2>)Camera.WorldToScreen);
        }

        protected override void OnSetupTypes(ISharedGlobalHandler handler)
        {
            
        }
    }
}
