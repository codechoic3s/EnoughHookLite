using System;
using System.Drawing;
using System.Numerics;
using static EnoughHookLite.Sys.WinAPI;

namespace EnoughHookLite.Sys
{
    public class Process
    {
        private IntPtr Handle;
        private IntPtr WindowHandle;

        public Vector2 Size;
        public Vector2 MidSize;
        public Point Position;

        private System.Diagnostics.Process Proc;

        public Process(System.Diagnostics.Process proc)
        {
            Proc = proc;
        }

        public static bool GetKeyState(VK key)
        {
            return (WinAPI.GetAsyncKeyState((int)key) & 0x8000) != 0;
        }

        public bool IsForeground()
        {
            return WindowHandle == WinAPI.GetForegroundWindow();
        }

        public static Process FindProcess(string name)
        {
            var procs = System.Diagnostics.Process.GetProcessesByName(name);
            if (procs.Length > 0)
            {
                return new Process(procs[0]);
            }
            else
                return null;
        }

        public bool AllocateHandles()
        {
            bool ok = true;
            try
            {
                Handle = Proc.Handle;
                WindowHandle = Proc.MainWindowHandle;
            }
            catch
            {
                ok = false;
            }
            return ok;
        }

        public byte[] ReadData(int adr, int size)
        {
            byte[] data = new byte[size];
            int readed = 0;
            var ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public Module GetModule(string name, out bool finded)
        {
            var mss = Proc.Modules;
            var msco = mss.Count;
            for (var i = 0; i < msco; i++)
            {
                var m = mss[i];
                if (m.ModuleName == name)
                {
                    finded = true;
                    return new Module(m, this);
                }
            }
            finded = false;
            return null;
        }
        
        public IntPtr SendMessage(int msg, int wparam, int lparam)
        {
            return WinAPI.SendMessage(WindowHandle, msg, wparam, lparam);
        }

        public void MoveCursor(Vector2 position, float accelx, float accely)
        {
            float x = (position.X - MidSize.X) / accelx;
            float y = (position.Y - MidSize.Y) / accely;
            WinAPI.mouse_event(32769U, (int)x, (int)y, 0U, 0);
        }

        public void UpdateWindow()
        {
            RECT rect;
            Point point = default;
            if (!WinAPI.ClientToScreen(WindowHandle, ref point))
            {
                Position = point;
                
            }
            if (!WinAPI.GetClientRect(WindowHandle, out rect))
            {
                Size.X = rect.Right - rect.Left;
                Size.Y = rect.Bottom - rect.Top;
                MidSize.X = Size.X / 2f;
                MidSize.Y = Size.Y / 2f;
            }
        }
    }
}
