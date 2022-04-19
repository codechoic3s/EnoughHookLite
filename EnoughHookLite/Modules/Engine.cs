using EnoughHookLite.Pointing;
using EnoughHookLite.Pointing.Attributes;
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
    public class Engine : ManagedModule
    {
        

        [Signature(SignaturesConsts.dwClientState)]
        private PointerCached pClientState;

        [Signature(SignaturesConsts.dwClientState_MaxPlayer)]
        private PointerCached pClientState_MaxPlayer;

        [Signature(SignaturesConsts.dwClientState_GetLocalPlayer)]
        private PointerCached pClientState_GetLocalPlayer;

        [Signature(SignaturesConsts.dwClientState_ViewAngels)]
        private PointerCached pClientState_ViewAngels;

        [Signature(SignaturesConsts.dwClientState_Map)]
        private PointerCached pClientState_Map;

        [Signature(SignaturesConsts.dwClientState_MapDirectory)]
        private PointerCached pClientState_MapDirectory;

        public uint ClientState { get { return NativeModule.Process.RemoteMemory.ReadUInt(NativeModule.BaseAdr + pClientState.Pointer); } }
        public int ClientState_MaxPlayers { get { return NativeModule.Process.RemoteMemory.ReadInt(ClientState + (uint)pClientState_MaxPlayer.Pointer); } }
        public int ClientState_GetLocalPlayer { get { return NativeModule.Process.RemoteMemory.ReadInt(ClientState + (uint)pClientState_GetLocalPlayer.Pointer); } }
        public Vector3 ClientState_ViewAngles { get { return NativeModule.Process.RemoteMemory.ReadStruct<Vector3>(ClientState + (uint)pClientState_ViewAngels.Pointer); } }
        public string ClientState_MapName { get { return NativeModule.Process.RemoteMemory.ReadString(ClientState + (uint)pClientState_Map.Pointer, 32, Encoding.ASCII); } }
        public string ClientState_MapDirectory { get { return NativeModule.Process.RemoteMemory.ReadString(ClientState + (uint)pClientState_MapDirectory.Pointer, 32, Encoding.ASCII); } }

        public Engine(Module m) : base(m)
        {
           
        }

        /*
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
        */
    }
}
