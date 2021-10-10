using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utils
{
    public class ConfigManager
    {
        private string CFGPath;

        public Config CurrentConfig { get; private set; }

        public Config[] Configs { get; private set; }

        public ConfigManager(string path)
        {
            CFGPath = path;
            LoadDefault();
        }

        public void LoadDefault()
        {
            CurrentConfig = new Config();
            CurrentConfig.Name = "None";
        }

        public void Save()
        {
            var skfc = CurrentConfig.Serialize();
            File.WriteAllText(CFGPath + '/' + CurrentConfig.Name, skfc);
        }

        public void Load(string name)
        {
            var cco = Configs.Length;
            for (var i = 0; i < cco; i++)
            {
                var kfc = Configs[i];
                if (kfc.Name == name)
                {
                    Load(i);
                    break;
                }
            }
        }

        public void Load(int id)
        {
            CurrentConfig = Configs[id];
        }

        public void UpdateList()
        {
            if (Directory.Exists(CFGPath))
            {
                List<Config> kfcs = new List<Config>();

                var filenames = Directory.GetFiles(CFGPath);

                var fco = filenames.Length;

                for (var i = 0; i < fco; i++)
                {
                    var fname = filenames[i];
                    var path = CFGPath + '/' + fname;
                    if (fname.EndsWith(".kfc") && File.Exists(path))
                    {
                        var kfc = Config.Deserialize(File.ReadAllText(path));
                        kfc.Name = fname;
                        kfcs.Add(kfc);
                    }
                }
                Configs = kfcs.ToArray();
            }
        }
    }
}
