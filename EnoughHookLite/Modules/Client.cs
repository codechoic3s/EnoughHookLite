using EnoughHookLite.GameClasses;
using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Modules
{
    public class Client
    {
        public Module ClientModule;
        public App App;

        public Client(Module m, App app)
        {
            ClientModule = m;
            App = app;
            EntityList = new EntityList(App);
        }

        public void Start()
        {
            EntityList.Start();
        }

        public EntityList EntityList;
        
        public int ClientState { get { return ClientModule.ReadInt(ClientModule.BaseAdr + Offsets.csgo.signatures.dwClientState); } }
        public int ClientState_MaxPlayers { get { return ClientModule.ReadInt(ClientState + Offsets.csgo.signatures.dwClientState_MaxPlayer); } }
        public int ClientState_GetLocalPlayer { get { return ClientModule.ReadInt(ClientState + Offsets.csgo.signatures.dwClientState_GetLocalPlayer); } }
        public Vector3 ClientState_ViewAngles { get { return ClientModule.ReadStruct<Vector3>(ClientState + Offsets.csgo.signatures.dwClientState_ViewAngles); } }

    }
}
