using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Modules
{
    public class Engine
    {
        public Module NativeModule { get; private set; }
        private App App;

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;

        public uint ClientState { get { return NativeModule.ReadUInt(NativeModule.BaseAdr + App.OffsetLoader.Offsets.Signatures.dwClientState); } }
        public int ClientState_MaxPlayers { get { return NativeModule.ReadInt(ClientState + (uint)App.OffsetLoader.Offsets.Signatures.dwClientState_MaxPlayer); } }
        public int ClientState_GetLocalPlayer { get { return NativeModule.ReadInt(ClientState + (uint)App.OffsetLoader.Offsets.Signatures.dwClientState_GetLocalPlayer); } }
        public Vector3 ClientState_ViewAngles { get { return NativeModule.ReadStruct<Vector3>(ClientState + (uint)App.OffsetLoader.Offsets.Signatures.dwClientState_ViewAngles); } }
        public string ClientState_MapName { get { return NativeModule.ReadString(ClientState + (uint)App.OffsetLoader.Offsets.Signatures.dwClientState_Map, 32, Encoding.ASCII); } }
        public string ClientState_MapDirectory { get { return NativeModule.ReadString(ClientState + (uint)App.OffsetLoader.Offsets.Signatures.dwClientState_MapDirectory, 32, Encoding.ASCII); } }

        public Engine(Module m, App app)
        {
            NativeModule = m;
            App = app;
        }

        public void LeftMouse(bool isdown)
        {
            if (isdown)
                LeftMouseDown();
            else
                LeftMouseUp();
        }
        public void LeftMouseDown()
        {
            NativeModule.Process.SendMessage(WM_LBUTTONDOWN, 1, 0); //mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }
        public void LeftMouseUp()
        {
            NativeModule.Process.SendMessage(WM_LBUTTONUP, 0, 0); //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public void Jump(bool state)
        {
            if (state)
            {
                NativeModule.Process.SendMessage(WM_KEYDOWN, 32, 3735553);

            }
            else
                NativeModule.Process.SendMessage(WM_KEYUP, 32, 3735553);
        }
    }
}
