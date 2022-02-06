using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public sealed class JSConfig
    {
        [JsonProperty("elements")]
        public Dictionary<string, object> Elements { get; internal set; }

        public JSConfig()
        {
            Elements = new Dictionary<string, object>();
        }

        public void SetValue(string name, object value)
        {
            if (!Elements.ContainsKey(name))
                Elements.Add(name, value);
            else
                Elements[name] = value;
        }

        public object GetValue(string name)
        {
            bool ok = Elements.TryGetValue(name, out object value);
            if (!ok)
                return null;
            else
                return value;
        }
    }
}
