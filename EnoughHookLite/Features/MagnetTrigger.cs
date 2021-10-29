using EnoughHookLite.GameClasses;
using EnoughHookLite.Sys;
using EnoughHookLite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Features
{
    public class MagnetTrigger
    {
        public App App;
        public Thread TH;
        public ManualResetEvent MRE;
        public TimeSpan TimeSpan;

        public MagnetTrigger(App app)
        {
            App = app;
            MRE = new ManualResetEvent(false);
            TimeSpan = TimeSpan.FromMilliseconds(0.5);
        }

        public void Start()
        {
            TH = new Thread(new ThreadStart(Work));
            TH.Start();
        }

        private void Work()
        {
            while (true)
            {
                //MRE.WaitOne(TimeSpan);
                if (App.CanNext && App.ConfigManager.CurrentConfig.Trigger.Enabled && Process.GetKeyState(App.ConfigManager.CurrentConfig.Trigger.Button))
                {
                    var player = App.Client.EntityList.LocalPlayer;
                    if (!(player.GetAimDirection().Length() < 0.001))
                    {
                        var aimRayWorld = new Line3D(player.EyePosition, player.EyePosition + player.GetAimDirection() * 8192);

                        foreach (var entity in App.Client.EntityList.CSPlayers)
                        {
                            if (!entity.IsAlive || entity.Pointer == player.Pointer)
                            {
                                continue;
                            }

                            // check if aim ray intersects any hitboxes of entity
                            var hitBoxId = IntersectsHitBox(aimRayWorld, entity);
                            if (hitBoxId >= 0)
                            {
                                // shoot
                                App.Engine.Fire(true);
                                App.Engine.Fire(false);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check if aim ray intersects any hitbox of entity.
        /// </summary>
        /// <returns>
        /// Returns id of intersected hitbox, otherwise -1.
        /// </returns>
        public static int IntersectsHitBox(Line3D aimRayWorld, CSPlayer entity)
        {
            for (var hitBoxId = 0; hitBoxId < entity.StudioHitBoxSet.numhitboxes; hitBoxId++)
            {
                var hitBox = entity.StudioHitBoxes[hitBoxId];
                var boneId = hitBox.bone;
                if (boneId < 0 || boneId > 128 || hitBox.radius <= 0)
                {
                    continue;
                }

                // intersect capsule
                var matrixBoneModelToWorld = entity.BonesMatrices[boneId];
                var boneStartWorld = matrixBoneModelToWorld.Transform(hitBox.bbmin);
                var boneEndWorld = matrixBoneModelToWorld.Transform(hitBox.bbmax);
                var boneWorld = new Line3D(boneStartWorld, boneEndWorld);
                var (p0, p1) = aimRayWorld.ClosestPointsBetween(boneWorld, true);
                var distance = (p1 - p0).Length();
                if (distance < hitBox.radius * 0.95f /* trigger a little bit inside */)
                {
                    // intersects
                    return hitBoxId;
                }
            }

            return -1;
        }
    }
}
