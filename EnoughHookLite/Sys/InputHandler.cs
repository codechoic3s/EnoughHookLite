using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Sys
{
    public sealed class InputHandler
    {
        //public const int WM_LBUTTONDOWN = 0x0201;
        //public const int WM_LBUTTONUP = 0x0202;

        //public const int WM_KEYDOWN = 0x0100;
        //public const int WM_KEYUP = 0x0101;
        //public const int WM_SETTEXT = 0X000C;

        public static bool GetKeyState(VK key)
        {
            return (WinAPI.GetAsyncKeyState((int)key) & 0x8000) != 0;
        }
        public static bool GetKeyState(int key)
        {
            return (WinAPI.GetAsyncKeyState(key) & 0x8000) != 0;
        }

        public static void SendLButtonDown(IntPtr hwnd)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_LBUTTONDOWN, 1, 0);
        }
        public static void SendLButtonUp(IntPtr hwnd)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_LBUTTONDOWN, 0, 0);
        }
        public static void SendRButtonDown(IntPtr hwnd)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_RBUTTONDOWN, 1, 0);
        }
        public static void SendRButtonUp(IntPtr hwnd)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_RBUTTONDOWN, 0, 0);
        }

        public static void SendString(IntPtr hwnd, string str)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_SETTEXT, 0, str);
        }

        public static void SendKeyDown(IntPtr hwnd, int vkey, uint scancodekey)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_KEYDOWN, vkey, scancodekey);
        }
        public static void SendKeyDown(IntPtr hwnd, int vkey, ScanCodeShort scancodekey)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_KEYDOWN, vkey, (int)scancodekey);
        }
        public static void SendKeyDown(IntPtr hwnd, VK vkey, uint scancodekey)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_KEYDOWN, (int)vkey, scancodekey);
        }
        public static void SendKeyDown(IntPtr hwnd, VK vkey, ScanCodeShort scancodekey)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_KEYDOWN, (int)vkey, (int)scancodekey);
        }

        public static void SendKeyUp(IntPtr hwnd, int vkey, uint scancodekey)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_KEYUP, vkey, scancodekey);
        }
        public static void SendKeyUp(IntPtr hwnd, int vkey, ScanCodeShort scancodekey)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_KEYUP, vkey, (int)scancodekey);
        }
        public static void SendKeyUp(IntPtr hwnd, VK vkey, uint scancodekey)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_KEYUP, (int)vkey, scancodekey);
        }
        public static void SendKeyUp(IntPtr hwnd, VK vkey, ScanCodeShort scancodekey)
        {
            WinAPI.SendMessage(hwnd, WMConsts.WM_KEYUP, (int)vkey, (int)scancodekey);
        }
    }
}
