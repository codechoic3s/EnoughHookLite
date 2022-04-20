using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public abstract class SharedAPI
    {
        public bool Installed { get; private set; }
        public void SetupAPI(ISharedHandler handler)
        {
            if (!Installed)
            {
                OnSetupAPI(handler);
                Installed = true;
            }
        }
        public abstract void OnSetupAPI(ISharedHandler handler);
    }
}
