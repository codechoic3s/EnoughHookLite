var cfg = config; // get config of script
// set default values of config
config.SetValue("on", false);

sync_config(); // sync with current configuration

var process = getProcess();
var client = getClient();
var engine = getEngine();
var localplayer = client.EntityList.LocalPlayer;

while (true)
{
    twait(1);
    if (!cfg.GetValue('on') || !getIsForeground())
    {
        twait(100);
        continue;
    }
    if (process.GetKeyState(VK.SPACE))
    {
        var flags = localplayer.FFlags;
        if (flags == 257 || flags == 263)
        {
            engine.Jump(true);
            engine.Jump(false);
        }
    }
}