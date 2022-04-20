using EnoughHookLite.OtherCode.Structs;
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace EnoughHookLite.Sys
{
    public class Process
    {
        internal IntPtr Handle;
        internal IntPtr WindowHandle;

        public Vector2 Size { get; private set; }
        public Vector2 MidSize { get; private set; }
        public Point Position { get; private set; }

        public Matrix MatrixViewport { get; private set; }

        public RemoteMemory RemoteMemory { get; private set; }

        private System.Diagnostics.Process Proc;

        public Process(System.Diagnostics.Process proc)
        {
            Proc = proc;
            RemoteMemory = new RemoteMemory(this);
        }

        public bool IsForeground()
        {
            return WindowHandle == WinAPI.GetForegroundWindow();
        }

        internal static Process FindProcess(string name)
        {
            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName(name);
            if (procs.Length > 0)
            {
                return new Process(procs[0]);
            }
            else
                return null;
        }

        internal bool AllocateHandles()
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

        public byte[] ReadData(IntPtr adr, int size)
        {
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }
        public byte[] ReadData(ulong adr, int size)
        {
            if (adr < 0)
                throw new Exception($"Not valid mapping!!! Ptr: {adr} Read size: {size}");
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }
        public byte[] ReadData(long adr, int size)
        {
            if (adr < 0)
                throw new Exception($"Not valid mapping!!! Ptr: {adr} Read size: {size}");
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }
        public byte[] ReadData(uint adr, int size)
        {
            if (adr < 0)
                throw new Exception($"Not valid mapping!!! Ptr: {adr} Read size: {size}");
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }
        public byte[] ReadData(int adr, int size)
        {
            if (adr < 0)
                throw new Exception($"Not valid mapping!!! Ptr: {adr} Read size: {size}");
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }
        public byte[] ReadData(IntPtr adr, uint size)
        {
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }
        public byte[] ReadData(ulong adr, uint size)
        {
            if (adr < 0)
                throw new Exception($"Not valid mapping!!! Ptr: {adr} Read size: {size}");
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }
        public byte[] ReadData(long adr, uint size)
        {
            if (adr < 0)
                throw new Exception($"Not valid mapping!!! Ptr: {adr} Read size: {size}");
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }
        public byte[] ReadData(uint adr, uint size)
        {
            if (adr < 0)
                throw new Exception($"Not valid mapping!!! Ptr: {adr} Read size: {size}");
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }
        public byte[] ReadData(int adr, uint size)
        {
            if (adr < 0)
                throw new Exception($"Not valid mapping!!! Ptr: {adr} Read size: {size}");
            byte[] data = new byte[size];
            int readed = 0;
            bool ok = WinAPI.ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                throw new Exception($"Native code: {Marshal.GetLastWin32Error()}, Ptr: {adr}, Read size: {size}, IsReaded={ok}, ReadedSize={readed}");
            }
        }

        public Module GetModule(string name, out bool finded)
        {
            System.Diagnostics.ProcessModuleCollection mss = Proc.Modules;
            int msco = mss.Count;
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
        public IntPtr SendMessage(int msg, int wparam, string lparam)
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
            Point point = default;
            if (WinAPI.ClientToScreen(WindowHandle, ref point))
            {
                Position = point;
            }
            if (WinAPI.GetClientRect(WindowHandle, out WinAPI.RECT rect))
            {
                var size = Size;
                size.X = rect.Right - rect.Left;
                size.Y = rect.Bottom - rect.Top;
                Size = size;
                var midsize = MidSize;
                midsize.X = size.X / 2f;
                midsize.Y = size.Y / 2f;
                MidSize = midsize;
            }
            //MatrixViewport = Utils.Math.GetMatrixViewport(Size);
        }
    }
}
