using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Modules
{
    public class Engine
    {
        public Module EngineModule;

        public Engine(Module m)
        {
            EngineModule = m;
        }

        public void Fire(bool state)
        {
            if (state)
                EngineModule.Process.SendMessage(WM_LBUTTONDOWN, 1, 0); //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            else
                EngineModule.Process.SendMessage(WM_LBUTTONUP, 0, 0);//mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        public void Jump(bool state)
        {
            if (state)
            {
                EngineModule.Process.SendMessage(256, 32, 3735553);

            }
            else
                EngineModule.Process.SendMessage(257, 32, 3735553);
        }
    }
}
