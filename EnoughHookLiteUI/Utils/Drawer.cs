using EnoughHookLiteUI.API;
using EnoughHookLiteUI.ScriptAPI;
using EnoughHookLiteUI.Windows;
using System;
using System.Collections.Generic;
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
        public Bitmap Bitmap { get; private set; }
        public Graphics Graphics { get; private set; }
        public WriteableBitmap WriteBitmap { get; private set; }

        public DrawAPI DrawAPI { get; private set; }
        
        public ulong Width { get; private set; }
        public ulong Height { get; private set; }

        public double FrameTime { get => FrameTimeFixed.TotalMilliseconds; set => FrameTimeFixed = TimeSpan.FromMilliseconds(value); }
        public double FrameRate { get => 1000.0 / FrameTime; set => FrameTime = 1000.0 / value; }

        private Thread CopyThread;
        private TimeSpan FrameTimeFixed;

        public Action DrawCall { get; set; }

        public Drawer(EHLWindow wnd)
        {
            Window = wnd;
        }
        public Drawer(ulong w, ulong h, EHLWindow wnd)
        {
            Window = wnd;
            Setup(w, h);
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
            if (DrawAPI is null)
                DrawAPI = new DrawAPI(Graphics, this, Window.DrawList);
            else
                DrawAPI.GFX = Graphics;
            WriteBitmap = new WriteableBitmap((int)Width, (int)Height, 96, 96, PixelFormats.Bgra32, null);
        }
        public void Start()
        {
            FrameRate = 60;
            CopyThread = new Thread(Work);
            CopyThread.Start();
        }

        private async void Work()
        {
            while (true)
            {
                if (DrawCall != null)
                {
                    Begin();
                    DrawCall();
                    End();
                    await Task.Delay(FrameTimeFixed);
                }
                else
                    await Task.Delay(1000);
            }
        }

        internal void Begin()
        {
            Graphics.Clear(System.Drawing.Color.Transparent);
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
        }
    }
}
