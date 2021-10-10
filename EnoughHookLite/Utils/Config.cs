using EnoughHookLite.Sys;
using EnoughHookLite.Utils.FeatureSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utils
{
    public class Config
    {
        public Trigger Trigger;
        public BunnyHop BunnyHop;

        [Newtonsoft.Json.JsonIgnore]
        public string Name;

        public Config()
        {
            Trigger = new Trigger(VK.SHIFT);
            BunnyHop = new BunnyHop(true);
        }

        public string Serialize()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static Config Deserialize(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(data);
        }
    }
}
