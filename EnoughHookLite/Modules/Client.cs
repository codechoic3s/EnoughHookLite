using EnoughHookLite.GameClasses;
using EnoughHookLite.Sys;
using EnoughHookLite.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Modules
{
    public class Client : ManagedModule
    {
        public Module NativeModule { get; private set; }
        public SubAPI SubAPI { get; private set; }

        private Thread ClientThread;

        public Client(Module m, SubAPI api) : base(m)
        {
            SubAPI = api;
            EntityList = new EntityList(SubAPI);
            Camera = new Camera(SubAPI);
            PlayerResource = new PlayerResource(this);
        }

        public void Start()
        {
            ClientThread = new Thread(Work);
            ClientThread.Start();
        }

        private void Work()
        {
            if (!SubAPI.TypesParser.TryParse(EntityList))
            {
                Console.WriteLine("Failed parse EntityList to offsets");
            }
            if (!SubAPI.TypesParser.TryParse(PlayerResource))
            {
                Console.WriteLine("Failed parse PlayerResource to offsets");
            }
            if (!SubAPI.TypesParser.TryParse(Camera))
            {
                Console.WriteLine("Failed parse Camera to offsets");
            }

            EntityList.FetchEntityList();
            PlayerResource.FetchMemoryAddress();
            Camera.ViewMatrixFetcher();
        }

        public EntityList EntityList { get; private set; }
        public PlayerResource PlayerResource { get; private set; }
        public Camera Camera { get; private set; }

        /*
        public bool InFOV(CSPlayer player, float FOV)
        {
            bool ok = false;
            for (var i = 0; i < 9; i++)
            {
                Vector2 vector = Camera.WorldToScreen(player.GetBonePosition(i));

                if (vector != Vector2.Zero)
                {
                    float dist = Vector2.Distance(SubAPI.Process.MidSize, vector);
                    if (dist < FOV)
                    {
                        ok = true;
                    }
                    else if (dist < FOV)
                    {
                        Vector2 vector3 = Camera.WorldToScreen(player.GetBonePosition(i));
                        if (Vector2.Distance(SubAPI.Process.MidSize, vector3) > dist)
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
            foreach (var item in EntityList.Entities)
            {
                if (item.Value is CSPlayer Player)
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
                    }
                }
            }
            return e;
        }
        
        public Vector2 GetAim()
        {
            var screenSize = SubAPI.Process.Size;
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
            var result = SubAPI.Process.MatrixViewport.Transform(pointClip);
            return new Vector2(result.X, result.Y);
        }
        */
        /*
        public Vector2 GetReverseAim()
        {
            var screenSize = SubAPI.Process.Size;
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
            var result = SubAPI.Process.MatrixViewport.Transform(pointClip);
            return new Vector2(result.X, result.Y);
        }
        */
        public Vector2 NormalizedAim(Vector2 vec2)
        {
            var n = new Vector2(vec2.X, vec2.Y);
            n.X -= SubAPI.Process.MidSize.X;
            n.Y -= SubAPI.Process.MidSize.Y;
            return n;
        }
    }
}
