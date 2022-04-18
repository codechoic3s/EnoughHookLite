var cfg = config; // get config of script
// set default values of config
config.SetValue("on", false);
config.SetValue("key", VK.SHIFT);

sync_config(); // sync with current configuration

var subapi = getSubAPI();

var process = subapi.Process;
var client = subapi.Client;
var engine = subapi.Engine;

var entitylist = client.EntityList;
var localplayer = entitylist.LocalPlayer;

while (true)
{
    twait(1);
    var ison = cfg.GetValue('on');
    if (!ison || !getIsForeground())
    {
        twait(100);
        continue;
    }
    var gkey = config.GetValue('key');
    if (process.GetKeyState(gkey))
    {
        var lccid = localplayer.CrosshairID;
        if (lccid != NaN && lccid > 0 && lccid < 64)
        {
            var entity = entitylist.GetByCrosshairID(lccid - 1);
            if (localplayer.Team != entity.Team)
            {
                engine.LeftMouseDown();
                if (!process.GetKeyState(VK.LBUTTON))
                    engine.LeftMouseUp();
            }
        }
    }   
}