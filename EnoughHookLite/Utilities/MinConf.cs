using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public sealed class MinConf<T> where T : SerializableConf<T>
    {
        public string LocationPath;

        public T Config;

        public MinConf(string location)
        {
            LocationPath = location;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(Config, Formatting.Indented);
        }

        public T Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void SerializeFile()
        {
            string ser = Serialize();
            File.WriteAllText(LocationPath, ser);
        }

        public void DeserializeFile()
        {
            if (File.Exists(LocationPath))
            {
                string json = File.ReadAllText(LocationPath);
                Config = Deserialize(json);
            }
            else
            {
                Config = Activator.CreateInstance<T>();
            }
        }
    }
}