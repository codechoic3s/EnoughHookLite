var cfg = config; // get config of script
// set default values of config
config.SetValue("on", false);

sync_config(); // sync with current configuration

var rm = getRemoteMemory();

var entitylist = getEntityList();

var localplayer = entitylist.LocalPlayer;

var pFFlags = getNetvar("DT_BasePlayer.m_fFlags");
var cached = localplayer.Pointer + pFFlags.Pointer;
while (true)
{
    twait(1);
    if (!cfg.GetValue('on'))
    {
        twait(100);
        continue;
    }
    
    if (getKeyStateVK(VK.SPACE))
    {
        var flags = rm.ReadIntInt(cached);
        if (flags == 257 || flags == 263)
        {
            sendKeyDown(VK.SPACE, ScanCodeShort.SPACE);
            sendKeyUp(VK.SPACE, ScanCodeShort.SPACE);
        }
    }
}