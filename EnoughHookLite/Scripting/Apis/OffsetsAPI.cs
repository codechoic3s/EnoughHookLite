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

        internal override void SetupAPI(ScriptLocal local)
        {
            local.Types.Add("SignaturesConsts", typeof(SignaturesConsts));

            local.Delegates.Add("getSignature", (Func<ulong, PointerCached>)AllocateSignature);
            local.Delegates.Add("getSignatureSC", (Func<SignaturesConsts, PointerCached>)AllocateSignature);
            local.Delegates.Add("getNetvar", (Func<string, PointerCached>)AllocateNetvar);
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
