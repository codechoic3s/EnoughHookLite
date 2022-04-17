using EnoughHookLite.Sys;
using EnoughHookLite.Utilities;
using EnoughHookLite.Utilities.ClientClassManaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing
{
    public sealed class PointManager
    {
        public SignatureManager SignatureManager { get; private set; }
        public ClientClassParser ClientClassParser { get; private set; }

        private Dictionary<ulong, PointerCached> Signatures;
        private Dictionary<string, PointerCached> Netvars;

        private SubAPI SubAPI;
        public PointManager(SubAPI subAPI)
        {
            SubAPI = subAPI;
        }

        public bool AllocateSignature(SignaturesConsts id, out PointerCached pc)
        {
            return Signatures.TryGetValue((ulong)id, out pc);
        }
        public bool AllocateSignature(ulong id, out PointerCached pc)
        {
            return Signatures.TryGetValue(id, out pc);
        }
        public bool AllocateSpecialNetvar(string id, out PointerCached pc)
        {
            if (!Netvars.TryGetValue(id, out pc))
            {
                string[] splited = id.Split('.');

                var datatable = ClientClassParser.DataTables[splited[0]];
                var variable = datatable.GetProperty(splited[1]);
                pc = new PointerCached(variable.Offset);
                Netvars.Add(id, pc);
                return true;
            }
            return true;
        }
        public bool AllocateNetvar(string id, out PointerCached pc)
        {
            if (!Netvars.TryGetValue(id, out pc))
            {
                string[] splited = id.Split('.');

                var datatable = ClientClassParser.DataTables[splited[0]];
                var variable = datatable.GetProperty(splited[1]);
                pc = new PointerCached(variable.Offset);
                Netvars.Add(id, pc);
                return true;
            }
            return true;
        }

        private void LogIt(string log)
        {
            App.Log.LogIt("[PointManager] " + log);
        }

        public bool InitClientClasses()
        {
            LogIt("Initing ClientClasses...");
            ClientClassParser = new ClientClassParser(SubAPI);
            if (!SubAPI.TypesParser.TryParse(ClientClassParser))
            {
                LogIt("Failed parse ClientClass for signatures.");
                return false;
            }
            ClientClassParser.Parsing();
            return true;
        }
        public void InitSignatures()
        {
            LogIt("Initing signatures...");
            SignatureManager = new SignatureManager(SubAPI);
            SignatureManager.LoadSignatures();
        }

        private void AllocateDefaultSignatures()
        {
            SignatureManager.SignatureList.Add("dwClientState", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 1 }, 0, true,
                0xA1, -1, -1, -1, -1, 0x33, 0xD2, 0x6A, 0x00, 0x6A, 0x00, 0x33, 0xC9, 0x89, 0xB0));

            SignatureManager.SignatureList.Add("dwClientState_GetLocalPlayer", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 2 }, 0, false,
                0x8B, 0x80, -1, -1, -1, -1, 0x40, 0xC3));

            SignatureManager.SignatureList.Add("dwClientState_IsHLTV", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 2 }, 0, false,
                0x80, 0xBF, -1, -1, -1, -1, -1, 0x0F, 0x84, -1, -1, -1, -1, 0x32, 0xDB));

            SignatureManager.SignatureList.Add("dwClientState_Map", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 1 }, 0, false,
                0x05, -1, -1, -1, -1, 0xC3, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xA1));

            SignatureManager.SignatureList.Add("dwClientState_MapDirectory", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 7 }, 0, false,
                0xB8, -1, -1, -1, -1, 0xC3, 0x05, -1, -1, -1, -1, 0xC3));

            SignatureManager.SignatureList.Add("dwClientState_MaxPlayer", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 7 }, 0, false,
                0xA1, -1, -1, -1, -1, 0x8B, 0x80, -1, -1, -1, -1, 0xC3, 0xCC, 0xCC, 0xCC, 0xCC, 0x55, 0x8B, 0xEC, 0x8A, 0x45, 0x08));

            SignatureManager.SignatureList.Add("dwClientState_PlayerInfo", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 2 }, 0, false,
                0x8B, 0x89, -1, -1, -1, -1, 0x85, 0xC9, 0x0F, 0x84, -1, -1, -1, -1, 0x8B, 0x01));

            SignatureManager.SignatureList.Add("dwClientState_State", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 2 }, 0, false,
                0x83, 0xB8, -1, -1, -1, -1, -1, 0x0F, 0x94, 0xC0, 0xC3));

            SignatureManager.SignatureList.Add("dwClientState_ViewAngles", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 4 }, 0, false,
                0xF3, 0x0F, 0x11, 0x86, -1, -1, -1, -1, 0xF3, 0x0F, 0x10, 0x44, 0x24, -1, 0xF3, 0x0F, 0x11, 0x86));

            SignatureManager.SignatureList.Add("clientstate_delta_ticks", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 2 }, 0, false,
                0xC7, 0x87, -1, -1, -1, -1, -1, -1, -1, -1, 0xFF, 0x15, -1, -1, -1, -1, 0x83, 0xC4, 0x08));

            SignatureManager.SignatureList.Add("clientstate_last_outgoing_command", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 2 }, 0, false,
                0x8B, 0x8F, -1, -1, -1, -1, 0x8B, 0x87, -1, -1, -1, -1, 0x41));

            SignatureManager.SignatureList.Add("clientstate_choked_commands", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 2 }, 0, false,
                0x8B, 0x87, -1, -1, -1, -1, 0x41));

            SignatureManager.SignatureList.Add("clientstate_net_channel", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 2 }, 0, false,
                0x8B, 0x8F, -1, -1, -1, -1, 0x8B, 0x01, 0x8B, 0x40, 0x18));

            SignatureManager.SignatureList.Add("dwEntityList", new Signature(SubAPI.Client.NativeModule,
               new int[] { 1 }, 0, true,
                0xBB, -1, -1, -1, -1, 0x83, 0xFF, 0x01, 0x0F, 0x8C, -1, -1, -1, -1, 0x3B, 0xF8));

            SignatureManager.SignatureList.Add("dwGameDir", new Signature(SubAPI.Engine.NativeModule,
               new int[] { 1 }, 0, true,
                0x68, -1, -1, -1, -1, 0x8D, 0x85, -1, -1, -1, -1, 0x50, 0x68, -1, -1, -1, -1, 0x68));

            SignatureManager.SignatureList.Add("dwGameRulesProxy", new Signature(SubAPI.Client.NativeModule,
               new int[] { 1 }, 0, true,
                0xA1, -1, -1, -1, -1, 0x85, 0xC0, 0x0F, 0x84, -1, -1, -1, -1, 0x80, 0xB8, -1, -1, -1, -1, -1, 0x74, 0x7A));

            SignatureManager.SignatureList.Add("dwGetAllClasses", new Signature(SubAPI.Client.NativeModule,
                new int[] { 1, 0 }, 0, true,
                0xA1, -1, -1, -1, -1, 0xC3, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xCC, 0xA1, -1, -1, -1, -1, 0xB9));

            SignatureManager.SignatureList.Add("dwGlobalVars", new Signature(SubAPI.Engine.NativeModule,
                new int[] { 1 }, 0, true,
                0x68, -1, -1, -1, -1, 0x68, -1, -1, -1, -1, 0xFF, 0x50, 0x08, 0x85, 0xC0));

            SignatureManager.SignatureList.Add("dwInput", new Signature(SubAPI.Client.NativeModule,
                new int[] { 1 }, 0, true,
                0xB9, -1, -1, -1, -1, 0xF3, 0x0F, 0x11, 0x04, 0x24, 0xFF, 0x50, 0x1));

            SignatureManager.SignatureList.Add("dwInterfaceLinkList", new Signature(SubAPI.Client.NativeModule,
                new int[] { }, 0, true,
                0x8B, 0x35, -1, -1, -1, -1, 0x57, 0x85, 0xF6, 0x74, -1, 0x8B, 0x7D, 0x08, 0x8B, 0x4E, 0x04, 0x8B, 0xC7, 0x8A, 0x11, 0x3A, 0x10));

            SignatureManager.SignatureList.Add("dwViewMatrix", new Signature(SubAPI.Client.NativeModule,
                new int[] { 3 }, 176, true,
                0x0F, 0x10, 0x05, -1, -1, -1, -1, 0x8D, 0x85, -1, -1, -1, -1, 0xB9));

            SignatureManager.SignatureList.Add("m_bDormant", new Signature(SubAPI.Client.NativeModule,
                new int[] { 2 }, 8, false,
                0x8A, 0x81, -1, -1, -1, -1, 0xC3, 0x32, 0xC0));

            SignatureManager.SignatureList.Add("is_c4_owner", new Signature(SubAPI.Client.NativeModule,
                new int[] { }, 0, true,
                0x56, 0x8B, 0xF1, 0x85, 0xF6, 0x74, 0x31));

            SignatureManager.SignatureList.Add("m_flSpawnTime", new Signature(SubAPI.Client.NativeModule,
                new int[] { 2 }, 0, false,
                0x89, 0x86, -1, -1, -1, -1, 0xE8, -1, -1, -1, -1, 0x80, 0xBE, -1, -1, -1, -1, -1));
        }
    }
}
