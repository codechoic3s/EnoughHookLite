using Jint.Runtime.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    /// <summary>
    /// Global class of initialization of customs & events.
    /// </summary>
    public sealed class JSApi
    {
        internal Dictionary<string, List<(string, Script)>> Callbacks;
        internal Dictionary<string, List<(string, Script)>> CustomCallbacks;

        private Dictionary<string, Type> CustomTypes;
        private Dictionary<string, Delegate> CustomDelegates;
        private Dictionary<string, object> CustomValues;

        internal Dictionary<string, object> GlobalValues;

        public JSApi()
        {
            Callbacks = new Dictionary<string, List<(string, Script)>>();
            CustomCallbacks = new Dictionary<string, List<(string, Script)>>();

            CustomTypes = new Dictionary<string, Type>();
            CustomDelegates = new Dictionary<string, Delegate>();
            CustomValues = new Dictionary<string, object>();

            GlobalValues = new Dictionary<string, object>();
        }

        public void ClearAllCustom()
        {
            CustomTypes.Clear();
            CustomDelegates.Clear();
            CustomValues.Clear();
        }

        public void AddType(string name, Type t)
        {
            CustomTypes.Add(name, t);
        }
        public void AddDelegate(string name, Delegate del)
        {
            CustomDelegates.Add(name, del);
        }
        public void AddValue(string name, object val)
        {
            CustomValues.Add(name, val);
        }
        public void AddEvent(string name, List<(string, Script)> delegates)
        {
            CustomCallbacks.Add(name, delegates);
        }

        public void RemoveEvent(JSEvent eve)
        {
            foreach (var item in Callbacks)
            {
                var list = item.Value;
                if (list.Contains(eve.LinkedDelegate))
                {
                    list.Remove(eve.LinkedDelegate);
                    break;
                }
            }
        }

        public void LoadAPI(Script script)
        {
            foreach (var item in CustomTypes)
            {
                script.JSEngine.SetValue(item.Key, TypeReference.CreateTypeReference(script.JSEngine, item.Value));
            }
            foreach (var item in CustomDelegates)
            {
                script.JSEngine.SetValue(item.Key, item.Value);
            }
            foreach (var item in CustomValues)
            {
                script.JSEngine.SetValue(item.Key, (dynamic)item.Value);
            }
        }
    }
}
