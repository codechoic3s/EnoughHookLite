using EnoughHookLite.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Apis
{
    public sealed class CameraAPI : SharedAPI
    {
        private Camera Camera;

        public CameraAPI(Camera cam)
        {
            Camera = cam;
        }

        public override void OnSetupAPI(ISharedHandler local)
        {
            local.AddDelegate("WorldToScreen", (Func<Vector3, Vector2>)Camera.WorldToScreen);
        }
    }
}
