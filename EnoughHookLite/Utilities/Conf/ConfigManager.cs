using EnoughHookLite.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.Conf
{
    public sealed class ConfigManager
    {
        internal string BasePath;
        public MinConf<MainConfig> Current { get; private set; }
        public MinConf<AModules> Modules { get; private set; }
        public MinConf<SignaturesConfig> Signatures { get; private set; }

        public ConfigManager(string bpath)
        {
            BasePath = bpath;
            Current = new MinConf<MainConfig>(BasePath + @"/config.json");
            Modules = new MinConf<AModules>(BasePath + @"/modules.json");
            Signatures = new MinConf<SignaturesConfig>(BasePath + @"/signatures.json");
        }

        internal void Load()
        {
            Modules.DeserializeFile();
            Modules.SerializeFile();

            Signatures.DeserializeFile();
            Signatures.SerializeFile();

            Current.DeserializeFile();
            Current.SerializeFile();
            LogIt("Config loaded");
        }

        internal void SyncWithCurrentScript(string name, Script script)
        {
            if (Current != null)
            {
                if (Current.Config.JSConfigs.TryGetValue(name, out ScriptConfig jsc))
                {
                    if (jsc.Elements.Count != script.Config.Elements.Count)
                    {
                        Current.Config.JSConfigs[name].Elements = script.Config.Elements;
                        Current.SerializeFile();
                    }
                    else
                    {
                        script.Config.Elements = Current.Config.JSConfigs[name].Elements;
                    }
                }
                else
                {
                    Current.Config.JSConfigs.Add(name, script.Config);
                    Current.SerializeFile();
                }
            }
        }

        private void LogIt(string log)
        {
            App.Log.LogIt("[ConfigManager] " + log);
        }
    }
}
