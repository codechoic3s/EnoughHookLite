using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;
using System.Windows.Threading;

namespace EnoughHookLiteUI
{
    public class Visualization
    {
        public static Visualization Create()
        {
            object[] ar = new object[2];
            var wndth = new Thread(new ParameterizedThreadStart((object obj) =>
            {
                var arr = (object[])obj;
                var twnd = new Visual();
                arr[0] = twnd;
                arr[1] = twnd.winform;
                obj = arr;
                twnd.ShowDialog();
            }));
            wndth.SetApartmentState(ApartmentState.STA);
            wndth.Start(ar);

            Visual wnd;
            while (ar[0] is null)
            {
                Thread.Sleep(1);
            }
            wnd = (Visual)ar[0];

            while (!wnd.isLoaded)
            {
                Thread.Sleep(1);
            }

            return new Visualization(wnd, wndth, (WindowsFormsHost)ar[1]);
        }


        private Visual Window;
        private WindowsFormsHost WinformHost;

        private Thread WindowThread;

        public Device Device { get; private set; }

        private Action RenderAction;

        public Action Renderable;

        private Visualization(Visual v, Thread wndthread, WindowsFormsHost wfh)
        {
            Window = v;
            WindowThread = wndthread;
            WinformHost = wfh;
            RenderAction = Render;
        }

        /// <summary>
        /// Initialize graphics device.
        /// </summary>
        public void InitDevice()
        {
            var parameters = new PresentParameters
            {
                Windowed = true,
                SwapEffect = SwapEffect.Discard,
                DeviceWindow = WinformHost.Child,
                MultiSampleQuality = 0,
                BackBufferFormat = Format.A8R8G8B8,
                BackBufferWidth = WinformHost.Child.Width,
                BackBufferHeight = WinformHost.Child.Height,
                EnableAutoDepthStencil = true,
                AutoDepthStencilFormat = DepthFormat.D16,
                PresentationInterval = PresentInterval.Immediate, // turn off v-sync
            };

            Device.IsUsingEventHandlers = true;
            Device = new Device(0, DeviceType.Hardware, WinformHost.Child, CreateFlags.HardwareVertexProcessing | CreateFlags.MultiThreaded, parameters);
        }

        public void DispatchRender()
        {
            var Do = Window.Dispatcher.BeginInvoke(RenderAction, DispatcherPriority.Render);
            Do.Wait();
        }

        private void Render()
        {
            // set render state
            Device.RenderState.AlphaBlendEnable = true;
            Device.RenderState.AlphaTestEnable = false;
            Device.RenderState.SourceBlend = Blend.SourceAlpha;
            Device.RenderState.DestinationBlend = Blend.InvSourceAlpha;
            Device.RenderState.Lighting = false;
            Device.RenderState.CullMode = Cull.None;
            Device.RenderState.ZBufferEnable = true;
            Device.RenderState.ZBufferFunction = Compare.Always;

            // clear scene
            Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.FromArgb(0, 0, 0, 0), 1, 0);

            // render scene
            Device.BeginScene();
            Renderable.Invoke();
            Device.EndScene();

            // flush to screen
            Device.Present();
        }
    }
}
