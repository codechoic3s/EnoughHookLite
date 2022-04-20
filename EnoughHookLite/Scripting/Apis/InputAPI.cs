using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Apis
{
    public sealed class InputAPI : SharedAPI
    {
        private Process Process;

        public InputAPI(Process pc)
        {
            Process = pc;
        }

        public override void OnSetupAPI(ISharedHandler local)
        {
            local.AddType("VK", typeof(VK));
            local.AddType("ScanCodeShort", typeof(ScanCodeShort));

            local.AddDelegate("sendKeyDown", (Action<VK, ScanCodeShort>)SendKeyDown);
            local.AddDelegate("sendKeyUp", (Action<VK, ScanCodeShort>)SendKeyUp);

            local.AddDelegate("sendLButtonDown", (Action)SendLButtonDown);
            local.AddDelegate("sendLButtonUp", (Action)SendLButtonUp);
            local.AddDelegate("sendRButtonDown", (Action)SendRButtonDown);
            local.AddDelegate("sendRButtonUp", (Action)SendRButtonUp);

            local.AddDelegate("sendString", (Action<string>)SendString);

            local.AddDelegate("getKeyStateInt", (Func<int, bool>)InputHandler.GetKeyState);
            local.AddDelegate("getKeyStateVK", (Func<VK, bool>)InputHandler.GetKeyState);
        }
        
        private void SendLButtonDown()
        {
            InputHandler.SendLButtonDown(Process.WindowHandle);
        }
        private void SendLButtonUp()
        {
            InputHandler.SendLButtonDown(Process.WindowHandle);
        }
        private void SendRButtonDown()
        {
            InputHandler.SendRButtonDown(Process.WindowHandle);
        }
        private void SendRButtonUp()
        {
            InputHandler.SendRButtonDown(Process.WindowHandle);
        }

        private void SendString(string str)
        {
            InputHandler.SendString(Process.WindowHandle, str);
        }
        private void SendKeyDown(VK vkey, ScanCodeShort scancode)
        {
            InputHandler.SendKeyDown(Process.WindowHandle, vkey, scancode);
        }
        private void SendKeyUp(VK vkey, ScanCodeShort scancode)
        {
            InputHandler.SendKeyUp(Process.WindowHandle, vkey, scancode);
        }
    }
}
