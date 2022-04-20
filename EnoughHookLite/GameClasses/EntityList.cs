using EnoughHookLite.Modules;
using EnoughHookLite.OtherCode;
using EnoughHookLite.OtherCode.Structs;
using EnoughHookLite.Pointing;
using EnoughHookLite.Pointing.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class EntityList
    {
        public Entity LocalPlayer => Entities[LocalPlayerID];
        public bool IsWorking { get; private set; }
        public int LocalPlayerID { get; private set; }
        public int Count => Entities.Count;

        internal Dictionary<int, Entity> Entities { get; private set; }
        private SubAPI SubAPI;
        [Signature(SignaturesConsts.dwEntityList)]
        private PointerCached pEntityList;

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
                int readptr = SubAPI.Client.NativeModule.BaseAdr + pEntityList.Pointer;
                Console.WriteLine(readptr);
                CEntInfo eentry = SubAPI.Process.RemoteMemory.ReadStruct<CEntInfo>(readptr);
                
                var centinfosize = Marshal.SizeOf<CEntInfo>();
                int eid = 0;
                int oldeid = 0;
                Console.WriteLine($"first {eid} - {eentry}");
                while (true)
                {
                    oldeid = eid;
                    eid++;
                    //eentry = SubAPI.Process.RemoteMemory.ReadStruct<CEntInfo>(readptr + (eid * centinfosize));

                    eentry = SubAPI.Process.RemoteMemory.ReadStruct<CEntInfo>(readptr);

                    readptr = eentry.pNext;
                    if (eentry.pNext == eentry.pPrevious || eentry.pNext == 0)
                        break;
                    if (eentry.pEntity == 0)
                        continue;
                    //Console.WriteLine($"{oldeid} - {eentry}");
                    //Console.WriteLine(eentry.pEntity);
                    /*
                    if (eentry.pNext <= 0 || eentry.pNext == eentry.pPrevious)
                    {
                        break;
                    }
                    if (eentry.pEntity <= 0)
                        continue;
                    */

                    eids.Add(oldeid);

                    var ent = UpdateEntityB(oldeid, eentry.pEntity);
                    
                    //Console.WriteLine($"added entity {preeid} {eentry.pEntity}");
                }

                RemoveNegativeEntities(eids.ToArray());
                eids.Clear();

                if (Entities.Count > 0) // fix
                {
                    // updating localplayer
                    LocalPlayerID = SubAPI.Engine.ClientState_GetLocalPlayer;
                    //Console.Title = SubAPI.Engine.ClientState_MapName;
                    //var localptr = App.Client.ClientModule.ReadInt(App.Client.ClientModule.BaseAdr + Offsets.App.OffsetLoader.Offsets.Signatures.dwLocalPlayer);
                }
                Console.WriteLine($"entities: {Entities.Count} lp: {LocalPlayerID}");
                await Task.Delay(100);
                //Thread.Sleep(3000);
            }
        }
    }
}
