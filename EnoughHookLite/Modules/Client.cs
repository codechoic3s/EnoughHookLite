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
            PlayerResource = new PlayerResource(this);
        }

        public void Start()
        {
            EntityList.Start();
            PlayerResource.Start();
        }

        public EntityList EntityList;
        public PlayerResource PlayerResource;

        public int ClientState { get { return ClientModule.ReadInt(ClientModule.BaseAdr + Offsets.csgo.signatures.dwClientState); } }
        public int ClientState_MaxPlayers { get { return ClientModule.ReadInt(ClientState + Offsets.csgo.signatures.dwClientState_MaxPlayer); } }
        public int ClientState_GetLocalPlayer { get { return ClientModule.ReadInt(ClientState + Offsets.csgo.signatures.dwClientState_GetLocalPlayer); } }
        public Vector3 ClientState_ViewAngles { get { return ClientModule.ReadStruct<Vector3>(ClientState + Offsets.csgo.signatures.dwClientState_ViewAngles); } }
        public string ClientState_MapName { get { return ClientModule.ReadString(ClientState + Offsets.csgo.signatures.dwClientState_Map, 32, Encoding.ASCII); } }
        public string ClientState_MapDirectory { get { return ClientModule.ReadString(ClientState + Offsets.csgo.signatures.dwClientState_MapDirectory, 32, Encoding.ASCII); } }

        public CSPlayer GetFovPlayer(float FOV)
        {
            CSPlayer e = null;
            foreach (CSPlayer Player in EntityList.CSPlayers)
            {
                if (Player.IsPlayer && Player.Team != EntityList.LocalPlayer.Team)
                {
                    Vector2 vector = App.Engine.WorldToScreen(Player.GetBonePosition(0));
                    
                    if (vector != Vector2.Zero)
                    {
                        float dist = Vector2.Distance(App.Process.MidSize, vector);
                        if (dist < FOV && e == null)
                        {
                            e = Player;
                        }
                        else if (dist < FOV)
                        {
                            Vector2 vector3 = App.Engine.WorldToScreen(Player.GetBonePosition(0));
                            Vector2 vector4;
                            vector4 = new Vector2(vector3.X, vector3.Y);
                            if (Vector2.Distance(App.Process.MidSize, vector4) > dist)
                            {
                                e = Player;
                            }
                        }
                    }
                }
            }
            return e;
        }
    }
}
