using EnoughHookLite;
using EnoughHookLite.Logging;
using EnoughHookLite.Scripting;
using EnoughHookLiteUI.API;
using EnoughHookLiteUI.Rendering;
using EnoughHookLiteUI.ScriptAPI;
using EnoughHookLiteUI.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EnoughHookLiteUI.Utils
{
    public sealed class Drawer
    {
        public EHLWindow Window { get; private set; }
        public Renderer Renderer { get; private set; }
        public Bitmap Bitmap { get; private set; }
        public Graphics Graphics { get; private set; }
        public WriteableBitmap WriteBitmap { get; private set; }

        public DrawAPI DrawAPI { get; private set; }
        
        public ulong Width { get; private set; }
        public ulong Height { get; private set; }

        public double FrameTime { get => FrameTimeFixed.TotalMilliseconds; set => FrameTimeFixed = TimeSpan.FromMilliseconds(value); }
        public double FrameRate { get => 1000.0 / FrameTime; set => FrameTime = 1000.0 / value; }
        public double MesureFrameTime;
        public double MesureFrameRate;

        private Thread RenderThread;
        private Thread ControlRenderThread;
        private TimeSpan FrameTimeFixed;

        private Stopwatch Mesure;
        private Task delay;
        private Task delay2;

        private LogEntry LogDrawer;

        public Action DrawCall { get; set; }

        public Drawer(EHLWindow wnd, Renderer rnd)
        {
            Window = wnd;
            Renderer = rnd;
            CurrentRender = PassiveRender;
            delay = Task.Delay(FrameTimeFixed);
            delay2 = Task.Delay(1000);

            LogDrawer = new LogEntry(() => { return "[Drawer] "; });
            App.LogHandler.AddEntry("Drawer", LogDrawer);
        }
        public void SetupDrawAPI(List<(string, Script)> drawlist)
        {
            DrawAPI = new DrawAPI(Graphics, this, drawlist); // setup customs
        }
        public void Setup(ulong w, ulong h)
        {
            if (w <= 0)
                w = 1;
            if (h <= 0)
                h = 1;

            Width = w;
            Height = h;
            Bitmap = new Bitmap((int)w, (int)h);
            Graphics = Graphics.FromImage(Bitmap);
            if (DrawAPI != null)
                DrawAPI.GFX = Graphics;
            WriteBitmap = new WriteableBitmap((int)Width, (int)Height, 96, 96, PixelFormats.Bgra32, null);
            Mesure = new Stopwatch();
        }
        public void Start()
        {
            FrameRate = 60;
            RenderThread = new Thread(Rendering);
            RenderThread.Start();
            ControlRenderThread = new Thread(Controlling);
            ControlRenderThread.Start();
        }

        private void Controlling()
        {
            while (true)
            {
                if (DrawCall != null && CurrentRender != ActiveRender)
                    CurrentRender = ActiveRender;
                else if (DrawCall == null && CurrentRender != PassiveRender)
                    CurrentRender = PassiveRender;
                Thread.Sleep(1000);
            }
        }
        private Func<Task> CurrentRender;
        private async Task ActiveRender()
        {
            Begin();
            DrawCall();
            End();
            await delay;
        }
        private async Task PassiveRender()
        {
            await delay2;
        }

        private async void Rendering()
        {
            while (true)
            {
                 await CurrentRender();
            }
        }

        internal void Begin()
        {
            Graphics.Clear(System.Drawing.Color.Transparent);
            Mesure.Restart();
        }

        internal void End()
        {
            Window.Dispatcher.Invoke(() =>
            {
                var w = (int)Width;
                var h = (int)Height;
                BitmapData data = Bitmap.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, Bitmap.PixelFormat);
                WriteBitmap.Lock();
                WinAPI.CopyMemory(WriteBitmap.BackBuffer, data.Scan0, (uint)((ulong)WriteBitmap.BackBufferStride * Height));
                WriteBitmap.AddDirtyRect(new Int32Rect(0, 0, w, h));
                WriteBitmap.Unlock();
                Bitmap.UnlockBits(data);
            });
            Mesure.Stop();
            MesureFrameTime = Mesure.Elapsed.TotalMilliseconds;
            MesureFrameRate = 1000.0 / MesureFrameTime;
        }
    }
}
