using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Sys
{
    public class Process
    {
        

        private IntPtr Handle;
        private IntPtr WindowHandle;

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

        public Module GetModule(string name)
        {
            var mss = Proc.Modules;
            var msco = mss.Count;
            for (var i = 0; i < msco; i++)
            {
                var m = mss[i];
                if (m.ModuleName == name)
                {
                    return new Module(m, this);
                }
            }
            return null;
        }
        
        public IntPtr SendMessage(int msg, int wparam, int lparam)
        {
            return WinAPI.SendMessage(WindowHandle, msg, wparam, lparam);
        }
    }
}
