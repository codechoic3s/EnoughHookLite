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
        public App App;

        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;

        public Engine(Module m, App app)
        {
            EngineModule = m;
            App = app;
        }

        public float[] ViewMatrix
        {
            get
            {
                float[] matrix = new float[16];
                for (var i = 0; i < 16; i++)
                {
                    matrix[i] = App.Client.ClientModule.ReadFloat(App.Client.ClientModule.BaseAdr + Offsets.csgo.signatures.dwViewMatrix + i * 4);
                }
                return matrix;
            }
        }

        public void Fire(bool state)
        {
            if (state)
                EngineModule.Process.SendMessage(WM_LBUTTONDOWN, 1, 0); //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            else
                EngineModule.Process.SendMessage(WM_LBUTTONUP, 0, 0); //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        public void Jump(bool state)
        {
            if (state)
            {
                EngineModule.Process.SendMessage(WM_KEYDOWN, 32, 3735553);

            }
            else
                EngineModule.Process.SendMessage(WM_KEYUP, 32, 3735553);
        }
    }
}
