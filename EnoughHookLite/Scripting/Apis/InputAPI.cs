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

        internal override void SetupAPI(ScriptLocal local)
        {
            local.Types.Add("VK", typeof(VK));
            local.Types.Add("ScanCodeShort", typeof(ScanCodeShort));

            local.Delegates.Add("sendKeyDown", (Action<VK, ScanCodeShort>)SendKeyDown);
            local.Delegates.Add("sendKeyUp", (Action<VK, ScanCodeShort>)SendKeyUp);

            local.Delegates.Add("sendLButtonDown", (Action)SendLButtonDown);
            local.Delegates.Add("sendLButtonUp", (Action)SendLButtonUp);
            local.Delegates.Add("sendRButtonDown", (Action)SendRButtonDown);
            local.Delegates.Add("sendRButtonUp", (Action)SendRButtonUp);

            local.Delegates.Add("sendString", (Action<string>)SendString);

            local.Delegates.Add("getKeyStateInt", (Func<int, bool>)InputHandler.GetKeyState);
            local.Delegates.Add("getKeyStateVK", (Func<VK, bool>)InputHandler.GetKeyState);
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
