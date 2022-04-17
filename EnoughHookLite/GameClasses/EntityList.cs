using EnoughHookLite.Modules;
using EnoughHookLite.OtherCode;
using EnoughHookLite.OtherCode.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class EntityList
    {
        public LocalPlayer LocalPlayer => (LocalPlayer)Entities[LocalPlayerID];
        public bool IsWorking { get; private set; }

        internal Dictionary<int, Entity> Entities { get; private set; }
        private SubAPI SubAPI;

        public int LocalPlayerID { get; private set; }

        public EntityList(SubAPI api)
        {
            SubAPI = api;
            Entities = new Dictionary<int, Entity>();
        }

        public Entity GetByID(int id)
        {
            return Entities[id];
        }

        internal Entity UpdateEntityB(int id, int ptr)
        {
            if (Entities.TryGetValue(id, out Entity entity))
                entity.Pointer = ptr;
            else 
            {
                entity = new Entity(SubAPI, ptr, id);
                Entities.Add(id, entity);
            }
            return entity;
        }
        internal void RemoveEntities(params int[] ids)
        {
            int idco = ids.Length;
            for (int i = 0; i < idco; i++)
            {
                Entities.Remove(ids[i]);
            }
        }
        internal void RemoveNegativeEntities(params int[] ids)
        {
            List<int> removeids = new List<int>();
            int idco = ids.Length;

            foreach (var item in Entities)
            {
                int key = item.Key;
                int reached = 0;
                for (int i = 0; i < idco; i++)
                {
                    if (key == ids[i])
                        reached++;
                }
                if (reached == 0)
                {
                    removeids.Add(key);
                }
            }

            RemoveEntities(removeids.ToArray());
        }

        internal async void FetchEntityList()
        {
            IsWorking = true;
            List<int> eids = new List<int>();
            while (IsWorking)
            {
                CEntInfo eentry;
                int readptr = SubAPI.Client.NativeModule.BaseAdr + App.OffsetLoader.Offsets.Signatures.dwEntityList;

                int eid = 0;
                while (true)
                {
                    eid++;
                    eentry = SubAPI.Client.NativeModule.Process.RemoteMemory.ReadStruct<CEntInfo>(readptr);
                    if (eentry.pNext == eentry.pPrevious)
                        break;
                    if (eentry.pEntity == 0)
                        continue;
                    eids.Add(eid);
                    var ent = UpdateEntityB(eid, eentry.pEntity);
                }

                RemoveNegativeEntities(eids.ToArray());
                eids.Clear();

                if (Entities.Count > 0) // fix
                {
                    // updating localplayer
                    LocalPlayerID = SubAPI.Engine.ClientState_GetLocalPlayer;
                    //var localptr = App.Client.ClientModule.ReadInt(App.Client.ClientModule.BaseAdr + Offsets.App.OffsetLoader.Offsets.Signatures.dwLocalPlayer);
                }

                await Task.Delay(3000);
                //Thread.Sleep(3000);
            }
        }
    }
}
