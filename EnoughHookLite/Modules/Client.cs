using EnoughHookLite.GameClasses;
using EnoughHookLite.Sys;
using EnoughHookLite.Utils;
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

        public bool InFOV(CSPlayer player, float FOV)
        {
            bool ok = false;
            for (var i = 0; i < 9; i++)
            {
                Vector2 vector = App.Engine.WorldToScreen(player.GetBonePosition(i));

                if (vector != Vector2.Zero)
                {
                    float dist = Vector2.Distance(App.Process.MidSize, vector);
                    if (dist < FOV)
                    {
                        ok = true;
                    }
                    else if (dist < FOV)
                    {
                        Vector2 vector3 = App.Engine.WorldToScreen(player.GetBonePosition(i));
                        if (Vector2.Distance(App.Process.MidSize, vector3) > dist)
                        {
                            ok = true;
                        }
                    }
                }
            }
            return ok;
        }
        public CSPlayer GetFovPlayer(float FOV)
        {
            CSPlayer e = null;
            foreach (CSPlayer Player in EntityList.CSPlayers)
            {
                if (!Player.IsNull 
                    && Player.Pointer != EntityList.LocalPlayer.Pointer 
                    && Player.IsPlayer 
                    && Player.Team != EntityList.LocalPlayer.Team
                    && Player.Health > 0)
                {
                    var infov = InFOV(Player, FOV);
                    if (infov)
                        e = Player;
                    /*
                    Vector2 vector = App.Engine.WorldToScreen(Player.GetBonePosition(8));
                    
                    if (vector != Vector2.Zero)
                    {
                        float dist = Vector2.Distance(App.Process.MidSize, vector);
                        if (dist < FOV && e == null)
                        {
                            e = Player;
                        }
                        else if (dist < FOV)
                        {
                            Vector2 vector3 = App.Engine.WorldToScreen(Player.GetBonePosition(8));
                            if (Vector2.Distance(App.Process.MidSize, vector3) > dist)
                            {
                                e = Player;
                            }
                        }
                    }
                    */
                }
            }
            return e;
        }
        public Vector2 GetAim()
        {
            var screenSize = App.Process.Size;
            var aspectRatio = (double)screenSize.X / screenSize.Y;
            var player = EntityList.LocalPlayer;
            var fovY = ((double)player.Fov).DegreeToRadian();
            var fovX = fovY * aspectRatio;
            var punchX = ((double)player.AimPunchAngle.X * 2.0).DegreeToRadian();
            var punchY = ((double)player.AimPunchAngle.Y * 2.0).DegreeToRadian();
            var pointClip = new Vector3
            (
                (float)(-punchY / fovX),
                (float)(-punchX / fovY),
                0
            );
            var result = App.Process.MatrixViewport.Transform(pointClip);
            return new Vector2(result.X, result.Y);
        }
        public Vector2 GetReverseAim()
        {
            var screenSize = App.Process.Size;
            var aspectRatio = (double)screenSize.X / screenSize.Y;
            var player = EntityList.LocalPlayer;
            var fovY = ((double)player.Fov).DegreeToRadian();
            var fovX = fovY * aspectRatio;
            var punchX = ((double)player.AimPunchAngle.X * 2.0).DegreeToRadian();
            var punchY = ((double)player.AimPunchAngle.Y * 2.0).DegreeToRadian();
            var pointClip = new Vector3
            (
                (float)(punchY / fovX),
                (float)(punchX / fovY),
                0
            );
            var result = App.Process.MatrixViewport.Transform(pointClip);
            return new Vector2(result.X, result.Y);
        }
        public Vector2 NormalizedAim(Vector2 vec2)
        {
            var n = new Vector2(vec2.X, vec2.Y);
            n.X -= App.Process.MidSize.X;
            n.Y -= App.Process.MidSize.Y;
            return n;
        }
    }
}
