using EnoughHookLite;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLiteUI
{
    public class VisualFeatures
    {
        private App App;
        private Visualization Visualization;

        private Thread RenderThread;

        private RenderInfo RenderInfo;

        private Microsoft.DirectX.Direct3D.Font FontVerdana8;

        private ManualResetEvent MRE;
        private TimeSpan TimeWait;

        public VisualFeatures(App app, Visualization v)
        {
            MRE = new ManualResetEvent(false);
            App = app;
            RenderInfo = new RenderInfo();
            Visualization = v;
            Visualization.Renderable = Renderable;
            FontVerdana8 = new Microsoft.DirectX.Direct3D.Font(Visualization.Device, new System.Drawing.Font("Verdana", 8.0f, FontStyle.Regular));
            TimeWait = TimeSpan.FromMilliseconds(0.5);
        }

        public void Start()
        {
            RenderThread = new Thread(new ThreadStart(Work));
            RenderThread.Start();
        }

        private void Work()
        {
            while (App.CanNext)
            {
                MRE.WaitOne(TimeWait);
                RenderInfo.Begin();

                Visualization.DispatchRender();

                RenderInfo.End();
            }
        }

        private void DrawRenderInfo()
        {
            FontVerdana8.DrawText(default, $"FPS: {RenderInfo.FrameRate}\n FrameTime: {RenderInfo.FrameTime}", 5, 5, Color.Orange);
        }

        private void DrawWindowBorder()
        {
            Visualization.Device.DrawPolyline(new[]
            {
                new Vector3(0, 0, 0),
                new Vector3(App.Process.Size.X - 1, 0, 0),
                new Vector3(App.Process.Size.X - 1, App.Process.Size.Y - 1, 0),
                new Vector3(0, App.Process.Size.Y - 1, 0),
                new Vector3(0, 0, 0),
            }, Color.LawnGreen);
        }

        private void Renderable()
        {
            DrawRenderInfo();
            DrawWindowBorder();
        }
    }
}
