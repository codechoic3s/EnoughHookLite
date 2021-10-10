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
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);
        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private IntPtr Handle;
        private IntPtr WindowHandle;

        private System.Diagnostics.Process Proc;

        public Process(System.Diagnostics.Process proc)
        {
            Proc = proc;
        }

        public static bool GetKeyState(VK key)
        {
            return (GetAsyncKeyState((int)key) & 0x8000) != 0;
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
            var ok = ReadProcessMemory(Handle, adr, data, size, ref readed);
            if (ok && readed == size)
            {
                return data;
            }
            else
            {
                return null;
            }
        }
        
        public IntPtr SendMessage(int msg, int wparam, int lparam)
        {
            return SendMessage(WindowHandle, msg, wparam, lparam);
        }
    }
}
