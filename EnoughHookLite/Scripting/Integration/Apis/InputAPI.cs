using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration.Apis
{
    public sealed class InputAPI : SharedAPI
    {
        private Process Process;

        public InputAPI(Process pc)
        {
            Process = pc;
        }

        protected override void OnSetupModule(ScriptModule module)
        {
            module.AddDelegate("sendKeyDown", (Action<VK, ScanCodeShort>)SendKeyDown);
            module.AddDelegate("sendKeyUp", (Action<VK, ScanCodeShort>)SendKeyUp);

            module.AddDelegate("sendLButtonDown", (Action)SendLButtonDown);
            module.AddDelegate("sendLButtonUp", (Action)SendLButtonUp);
            module.AddDelegate("sendRButtonDown", (Action)SendRButtonDown);
            module.AddDelegate("sendRButtonUp", (Action)SendRButtonUp);

            module.AddDelegate("sendString", (Action<string>)SendString);

            module.AddDelegate("getKeyStateInt", (Func<int, bool>)InputHandler.GetKeyState);
            module.AddDelegate("getKeyStateVK", (Func<VK, bool>)InputHandler.GetKeyState);
        }

        protected override void OnSetupTypes(ISharedGlobalHandler handler)
        {
            handler.AddType("VK", typeof(VK));
            handler.AddType("ScanCodeShort", typeof(ScanCodeShort));
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
