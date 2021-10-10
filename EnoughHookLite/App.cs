using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnstableTrigger
{
    public class Unstable
    {
        


        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        public static int Readint(byte[] data)
        {
            return BitConverter.ToInt32(data, 0);
        }

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        //private const int MOUSEEVENTF_LEFTDOWN = 2; //FLag for pressed button
        //private const int MOUSEEVENTF_LEFTUP = 4; //Flag for released button

        

        public IntPtr ClientModulePtr;
        public int ClientModuleSize;

        public IntPtr Handle;
        public IntPtr HWND;
        public Process CSGO;

        public bool AllocateProcess()
        {
            bool state = false;
            var procs = Process.GetProcessesByName("csgo");
            if (procs.Length > 0)
            {
                state = true;
                CSGO = procs[0];
                Handle = CSGO.Handle;
                HWND = CSGO.MainWindowHandle;
                var modules = CSGO.Modules;
                var mco = modules.Count;
                bool findedmodule = false;
                for (var i = 0; i < mco; i++)
                {
                    var m = modules[i];
                    if (m.ModuleName == "client.dll")
                    {
                        ClientModulePtr = m.BaseAddress;
                        ClientModuleSize = m.ModuleMemorySize;
                        findedmodule = true;
                    }
                }
                state = findedmodule;
            }
            return state;
        }

        public Thread TH;
        public void Start()
        {
            TH = new Thread(new ThreadStart(Work));
            TH.Start();
        }
        private void Work()
        {
            while (true)
            {
                LocalPlayerInstance = RPMInt(ClientModulePtr + LocalPlayerPTR);
                Thread.Sleep(500);
            }
        }

        public void LoadOffsets()
        {
            var file = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"/offsets.txt");
            LocalPlayerPTR = int.Parse(file[0]);
            CrosshairIDPTR = int.Parse(file[1]);
            TeamIDPTR = int.Parse(file[2]);
            EntityListPTR = int.Parse(file[3]);
            FlagsPTR = int.Parse(file[4]);
        }

        
    }
}
