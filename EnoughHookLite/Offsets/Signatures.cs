using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Offsets
{
    public sealed class Signatures
    {
        [JsonProperty("anim_overlays")]
        public Int32 anim_overlays = 0x2990;
        [JsonProperty("clientstate_choked_commands")]
        public Int32 clientstate_choked_commands = 0x4D30;
        [JsonProperty("clientstate_delta_ticks")]
        public Int32 clientstate_delta_ticks = 0x174;
        [JsonProperty("clientstate_last_outgoing_command")]
        public Int32 clientstate_last_outgoing_command = 0x4D2C;
        [JsonProperty("clientstate_net_channel")]
        public Int32 clientstate_net_channel = 0x9C;
        [JsonProperty("convar_name_hash_table")]
        public Int32 convar_name_hash_table = 0x2F0F8;
        [JsonProperty("dwClientState")]
        public Int32 dwClientState = 0x58BFC4;
        [JsonProperty("dwClientState_GetLocalPlayer")]
        public Int32 dwClientState_GetLocalPlayer = 0x180;
        [JsonProperty("dwClientState_IsHLTV")]
        public Int32 dwClientState_IsHLTV = 0x4D48;
        [JsonProperty("dwClientState_Map ")]
        public Int32 dwClientState_Map = 0x28C;
        [JsonProperty("dwClientState_MapDirectory")]
        public Int32 dwClientState_MapDirectory = 0x188;
        [JsonProperty(" dwClientState_MaxPlayer")]
        public Int32 dwClientState_MaxPlayer = 0x388;
        [JsonProperty("dwClientState_PlayerInfo")]
        public Int32 dwClientState_PlayerInfo = 0x52C0;
        [JsonProperty("dwClientState_State")]
        public Int32 dwClientState_State = 0x108;
        [JsonProperty("dwClientState_ViewAngles")]
        public Int32 dwClientState_ViewAngles = 0x4D90;
        [JsonProperty("dwEntityList")]
        public Int32 dwEntityList = 0x4DCEB7C;
        [JsonProperty("dwForceAttack")]
        public Int32 dwForceAttack = 0x31FF054;
        [JsonProperty("dwForceAttack2")]
        public Int32 dwForceAttack2 = 0x31FF060;
        [JsonProperty("dwForceBackward")]
        public Int32 dwForceBackward = 0x31FF0A8;
        [JsonProperty("dwForceForward")]
        public Int32 dwForceForward = 0x31FF09C;
        [JsonProperty("dwForceJump")]
        public Int32 dwForceJump = 0x52789F8;
        [JsonProperty("dwForceLeft")]
        public Int32 dwForceLeft = 0x31FF0B4;
        [JsonProperty("dwForceRight")]
        public Int32 dwForceRight = 0x31FF0C0;
        [JsonProperty("dwGameDir")]
        public Int32 dwGameDir = 0x62A880;
        [JsonProperty("dwGameRulesProxy")]
        public Int32 dwGameRulesProxy = 0x52EBA5C;
        [JsonProperty("dwGetAllClasses")]
        public Int32 dwGetAllClasses = 0xDDCF2C;
        [JsonProperty("dwGlobalVars")]
        public Int32 dwGlobalVars = 0x58BCC8;
        [JsonProperty("dwGlowObjectManager")]
        public Int32 dwGlowObjectManager = 0x5316E98;
        [JsonProperty("dwInput")]
        public Int32 dwInput = 0x5220150;
        [JsonProperty("dwInterfaceLinkList")]
        public Int32 dwInterfaceLinkList = 0x966044;
        [JsonProperty("dwLocalPlayer")]
        public Int32 dwLocalPlayer = 0xDB35EC;
        [JsonProperty("dwMouseEnable")]
        public Int32 dwMouseEnable = 0xDB92F8;
        [JsonProperty("dwMouseEnablePtr")]
        public Int32 dwMouseEnablePtr = 0xDB92C8;
        [JsonProperty("dwPlayerResource")]
        public Int32 dwPlayerResource = 0x31FD3E0;
        [JsonProperty("dwRadarBase")]
        public Int32 dwRadarBase = 0x52038F4;
        [JsonProperty("dwSensitivity")]
        public Int32 dwSensitivity = 0xDB9194;
        [JsonProperty("dwSensitivityPtr")]
        public Int32 dwSensitivityPtr = 0xDB9168;
        [JsonProperty("dwSetClanTag")]
        public Int32 dwSetClanTag = 0x8A290;
        [JsonProperty("dwViewMatrix")]
        public Int32 dwViewMatrix = 0x4DC0494;
        [JsonProperty("dwWeaponTable")]
        public Int32 dwWeaponTable = 0x5220C18;
        [JsonProperty("dwWeaponTableIndex")]
        public Int32 dwWeaponTableIndex = 0x326C;
        [JsonProperty("dwYawPtr")]
        public Int32 dwYawPtr = 0xDB8F58;
        [JsonProperty("dwZoomSensitivityRatioPtr")]
        public Int32 dwZoomSensitivityRatioPtr = 0xDBEF60;
        [JsonProperty("dwbSendPackets")]
        public Int32 dwbSendPackets = 0xD93D2;
        [JsonProperty("dwppDirect3DDevice9")]
        public Int32 dwppDirect3DDevice9 = 0xA5050;
        [JsonProperty("find_hud_element")]
        public Int32 find_hud_element = 0x283D48A0;
        [JsonProperty("force_update_spectator_glow")]
        public Int32 force_update_spectator_glow = 0x3BA6CA;
        [JsonProperty("interface_engine_cvar")]
        public Int32 interface_engine_cvar = 0x3E9EC;
        [JsonProperty("is_c4_owner")]
        public Int32 is_c4_owner = 0x3C76A0;
        [JsonProperty("m_bDormant")]
        public Int32 m_bDormant = 0xED;
        [JsonProperty("m_flSpawnTime")]
        public Int32 m_flSpawnTime = 0x103C0;
        [JsonProperty("m_pStudioHdr")]
        public Int32 m_pStudioHdr = 0x2950;
        [JsonProperty("m_pitchClassPtr")]
        public Int32 m_pitchClassPtr = 0x5203B90;
        [JsonProperty("m_yawClassPtr")]
        public Int32 m_yawClassPtr = 0xDB8F58;
        [JsonProperty("model_ambient_min")]
        public Int32 model_ambient_min = 0x58F03C;
        [JsonProperty("set_abs_angles")]
        public Int32 set_abs_angles = 0x1E51E0;
        [JsonProperty("set_abs_origi")]
        public Int32 set_abs_origin = 0x1E5020;
    }
}
