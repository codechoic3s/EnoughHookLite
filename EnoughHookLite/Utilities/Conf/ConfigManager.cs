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
        internal string ConfigsPath;
        internal MinConf<EHLConfig> Current;
        internal MinConf<AModules> Modules;

        public ConfigManager(string cpath)
        {
            ConfigsPath = cpath;
            Current = new MinConf<EHLConfig>(ConfigsPath);
        }

        internal void Load()
        {
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
