using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
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

        public TimeSpan TimeSpan;
        private Thread TH;
        private ManualResetEvent MRE;

        public Engine(Module m, App app)
        {
            EngineModule = m;
            App = app;
            MRE = new ManualResetEvent(false);
        }

        public void Start()
        {
            TimeSpan = TimeSpan.FromMilliseconds(5);
            TH = new Thread(new ThreadStart(Work));
            TH.Start();
        }

        private void Work()
        {
            ViewMatrix = new float[16];
            while (true)
            {
                MRE.WaitOne(TimeSpan);
                for (var i = 0; i < 16; i++)
                {
                    ViewMatrix[i] = App.Client.ClientModule.ReadFloat(App.Client.ClientModule.BaseAdr + Offsets.csgo.signatures.dwViewMatrix + i * 4);
                }
            }
        }

        public float[] ViewMatrix { get; private set; }

        public Vector2 WorldToScreen(Vector3 target)
        {
            Vector2 _worldToScreenPos;
            Vector3 to;
            float w = 0.0f;
            float[] viewmatrix = ViewMatrix;

            to.X = viewmatrix[0] * target.X + viewmatrix[1] * target.Y + viewmatrix[2] * target.Z + viewmatrix[3];
            to.Y = viewmatrix[4] * target.X + viewmatrix[5] * target.Y + viewmatrix[6] * target.Z + viewmatrix[7];

            w = viewmatrix[12] * target.X + viewmatrix[13] * target.Y + viewmatrix[14] * target.Z + viewmatrix[15];

            // behind us
            if (w < 0.01f)
                return new Vector2(0, 0);

            to.X *= (1.0f / w);
            to.Y *= (1.0f / w);

            float width = App.Process.MidSize.X;
            float height = App.Process.MidSize.Y;

            float x = width / 2;
            float y = height / 2;

            x += 0.5f * to.X * width + 0.5f;
            y -= 0.5f * to.Y * height + 0.5f;

            to.X = x;
            to.Y = y;

            _worldToScreenPos.X = to.X;
            _worldToScreenPos.Y = to.Y;
            return _worldToScreenPos;
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
