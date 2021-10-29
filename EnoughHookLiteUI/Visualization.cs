using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLiteUI
{
    public class Visualization
    {
        public static Visualization Create()
        {
            object[] ar = new object[1];
            var wndth = new Thread(new ParameterizedThreadStart((object obj) =>
            {
                var arr = (object[])obj;
                var twnd = new Visual();
                arr[0] = twnd;
                obj = arr;
                twnd.ShowDialog();
            }));
            wndth.Start(ar);

            Visual wnd;
            while (ar[0] is null)
            {
                Thread.Sleep(1);
            }
            wnd = (Visual)ar[0];

            while (!wnd.IsLoaded)
            {
                Thread.Sleep(1);
            }

            return new Visualization(wnd, wndth);
        }


        private Visual Window;
        private Thread WindowThread;

        private Device Device;

        /// <summary>
        /// Initialize graphics device.
        /// </summary>
        private void InitDevice()
        {
            var parameters = new PresentParameters
            {
                Windowed = true,
                SwapEffect = SwapEffect.Discard,
                DeviceWindow = WindowOverlay.Window,
                MultiSampleQuality = 0,
                BackBufferFormat = Format.A8R8G8B8,
                BackBufferWidth = WindowOverlay.Window.Width,
                BackBufferHeight = WindowOverlay.Window.Height,
                EnableAutoDepthStencil = true,
                AutoDepthStencilFormat = DepthFormat.D16,
                PresentationInterval = PresentInterval.Immediate, // turn off v-sync
            };

            Device.IsUsingEventHandlers = true;
            Device = new Device(0, DeviceType.Hardware, WindowOverlay.Window, CreateFlags.HardwareVertexProcessing, parameters);
        }

        private Visualization(Visual v, Thread wndthread)
        {
            Window = v;
            WindowThread = wndthread;
        }

        public void LoadGraphics()
        {

        }
    }
}
