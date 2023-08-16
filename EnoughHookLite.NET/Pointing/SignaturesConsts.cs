using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing
{
    public enum SignaturesConsts
    {
        dwClientState,
        dwClientState_GetLocalPlayer,
        dwClientState_IsHLTV,
        dwClientState_Map,
        dwClientState_MapDirectory,
        dwClientState_MaxPlayer,
        dwClientState_PlayerInfo,
        dwClientState_State,
        dwClientState_ViewAngels,

        clientstate_delta_ticks,
        clientstate_last_outgoing_command,
        clientstate_choked_commands,
        clientstate_net_channel,

        dwPlayerResource,
        dwEntityList,
        dwGameDir,
        dwGameRulesProxy,
        dwGetAllClasses,
        dwGlobalVars,
        dwInput,
        dwInterfaceLinkList,
        dwViewMatrix,

        m_bDormant,
        is_c4_owner,
        m_flSpawnTime,
    }
}
