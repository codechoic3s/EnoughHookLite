using EnoughHookLite.Pointing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Apis
{
    public sealed class OffsetsAPI : SharedAPI
    {
        private PointManager PointManager;

        public OffsetsAPI(PointManager pm)
        {
            PointManager = pm;
        }

        public override void OnSetupAPI(ISharedHandler local)
        {
            local.AddType("SignaturesConsts", typeof(SignaturesConsts));

            local.AddDelegate("getSignature", (Func<ulong, PointerCached>)AllocateSignature);
            local.AddDelegate("getSignatureSC", (Func<SignaturesConsts, PointerCached>)AllocateSignature);
            local.AddDelegate("getNetvar", (Func<string, PointerCached>)AllocateNetvar);
        }

        private PointerCached AllocateSignature(SignaturesConsts id)
        {
            PointerCached pointer = null;
            PointManager.AllocateSignature(id, out pointer);
            return pointer;
        }
        private PointerCached AllocateSignature(ulong id)
        {
            PointerCached pointer = null;
            PointManager.AllocateSignature(id, out pointer);
            return pointer;
        }
        private PointerCached AllocateNetvar(string id)
        {
            PointerCached pointer = null;
            PointManager.AllocateNetvar(id, out pointer);
            return pointer;
        }
    }
}
