using EnoughHookLiteUI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.Rendering
{
    public sealed class ManualRenderer
    {
        public WindowsRenderer WindowsRenderer { get; set; }
        public CustomRenderer CustomRenderer { get; set; }

        //private Thread ControlThread;

        public Renderer Renderer { get; private set; }

        public ManualRenderer(EHLWindow wnd)
        {
            WindowsRenderer = new WindowsRenderer();
            CustomRenderer = new CustomRenderer();

            Renderer = new Renderer(wnd);
            Renderer.AddRender(0, CustomRenderer);
            Renderer.AddRender(1, WindowsRenderer);

            Renderer.Drawer.SetupDrawAPI(CustomRenderer.DrawList);
        }
        /*
        public void Start()
        {
            
            ControlThread = new Thread(Controlling);
            ControlThread.IsBackground = true;
            ControlThread.Start();
            
        }

        private void Controlling()
        {
            while (true)
            {
                Thread.Sleep(50);
            }
        }
        */
    }
}
