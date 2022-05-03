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
        public Entity LocalPlayer { get; private set; }
        public bool IsWorking { get; private set; }
        public int Count => Entities.Count;

        internal Dictionary<int, Entity> Entities { get; private set; }
        private SubAPI SubAPI;
        [Signature(SignaturesConsts.dwEntityList)]
        private PointerCached pEntityList;

        public Entity[] pEntities => Entities.Values.ToArray();

        public EntityList(SubAPI api)
        {
            SubAPI = api;
            Entities = new Dictionary<int, Entity>();
            LocalPlayer = new Entity(api, 0, 0);
        }

        public Entity GetByID(int id)
        {
            return Entities[id];
        }

        internal Entity UpdateEntityB(int id, uint ptr)
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
        private void LogIt(string log)
        {
            App.Log.LogIt("[EntityList] " + log);
        }
        private void ParseCEntInfo(CEntInfo info, ref uint prev)
        {

        }
        internal async void FetchEntityList()
        {
            try
            {
                IsWorking = true;
                List<int> eids = new List<int>();
                while (IsWorking)
                {
                    uint readptr = SubAPI.Client.NativeModule.BaseAdr + pEntityList.Pointer;
                    CEntInfo eentry;
                    //uint centinfosize = (uint)Marshal.SizeOf<CEntInfo>();
                    //uint eid = 0;
                    //int oldeid = 0;
                    //LogIt($"first {eid} - {eentry}");
                    uint prev = 0;
                    while (true)
                    {
                        //eentry = SubAPI.Process.RemoteMemory.ReadStruct<CEntInfo>(readptr + (eid * centinfosize));
                        //eid++;

                        eentry = SubAPI.Process.RemoteMemory.ReadStruct<CEntInfo>(readptr);

                        if (eentry.pNext == eentry.pPrevious || eentry.pNext == 0)
                            break;
                        readptr = eentry.pNext;
                        if (eentry.pEntity == 0)
                            continue;
                        prev = eentry.pEntity;
                        var ind = SubAPI.Process.RemoteMemory.ReadInt(eentry.pEntity + 0x64) - 1;

                        var ent = UpdateEntityB(ind, eentry.pEntity);
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

                        eids.Add(ind);
                        //Console.WriteLine($"added entity {preeid} {eentry.pEntity}");
                    }

                    RemoveNegativeEntities(eids.ToArray());
                    eids.Clear();

                    if (Entities.Count > 0) // fix
                    {
                        // updating localplayer
                        LocalPlayer.Index = SubAPI.Engine.ClientState_GetLocalPlayer;
                        LocalPlayer.Pointer = Entities[LocalPlayer.Index].Pointer;
                        //Console.Title = SubAPI.Engine.ClientState_MapName;
                        //var localptr = App.Client.ClientModule.ReadInt(App.Client.ClientModule.BaseAdr + Offsets.App.OffsetLoader.Offsets.Signatures.dwLocalPlayer);
                    }
                    //Console.WriteLine($"entities: {Entities.Count} lp: {LocalPlayerID}");
                    await Task.Delay(500);
                    //Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
                LogIt(ex.ToString());
            }
        }
    }
}
