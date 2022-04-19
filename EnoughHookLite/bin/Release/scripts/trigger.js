var cfg = config; // get config of script
// set default values of config
config.SetValue("on", false);
config.SetValue("key", VK.SHIFT);

sync_config(); // sync with current configuration

var rm = getRemoteMemory();

var entitylist = getEntityList();

var localplayer = entitylist.LocalPlayer;

var pTeam = getNetvar("DT_BaseEntity.m_iTeamNum");
log("aboba");
var pCrosshairID = getNetvar("DT_CSPlayer.m_bHasDefuser");

while (true)
{
    twait(1);
    var ison = cfg.GetValue('on');
    if (!ison)
    {
        twait(100);
        continue;
    }

    var gkey = config.GetValue('key');
    if (getKeyStateVK(gkey))
    {
        
        var lccid = rm.ReadIntInt(localplayer.Pointer + pCrosshairID.Pointer + 92);
        
        if (lccid != NaN && lccid > 0 && lccid < 64)
        {
            var entity = entitylist.GetByID(lccid - 1);

            entteam = rm.ReadIntInt(entity.Pointer + pTeam.Pointer);
            lcteam = rm.ReadIntInt(localplayer.Pointer + pTeam.Pointer);

            if (lcteam != entteam)
            {
                sendLButtonDown();
                if (!getKeyStateVK(VK.LBUTTON))
                    sendLButtonUp();
            }
        }
    }   
}