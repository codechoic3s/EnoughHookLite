using EnoughHookLite.Pointing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration.Apis
{
    public sealed class OffsetsAPI : SharedAPI
    {
        private PointManager PointManager;

        public OffsetsAPI(PointManager pm)
        {
            PointManager = pm;
        }

        protected override void OnSetupModule(ScriptModule module)
        {
            module.AddDelegate("getSignature", (Func<ulong, PointerCached>)AllocateSignature);
            module.AddDelegate("getSignatureSC", (Func<SignaturesConsts, PointerCached>)AllocateSignature);
            module.AddDelegate("getNetvar", (Func<string, PointerCached>)AllocateNetvar);
        }

        protected override void OnSetupTypes(ISharedGlobalHandler handler)
        {
            handler.AddType("SignaturesConsts", typeof(SignaturesConsts));
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
