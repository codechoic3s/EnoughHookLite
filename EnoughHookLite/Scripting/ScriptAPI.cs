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
    public sealed class ScriptAPI : ISharedHandler, ISharedLoader
    {
        internal Dictionary<string, List<(string, Script)>> Callbacks;
        internal Dictionary<string, List<(string, Script)>> CustomCallbacks;

        private Dictionary<string, Type> CustomTypes;
        private Dictionary<string, Delegate> CustomDelegates;
        private Dictionary<string, object> CustomValues;

        internal Dictionary<string, object> GlobalValues;
        internal List<SharedAPI> SharedApis;

        public ScriptAPI()
        {
            Callbacks = new Dictionary<string, List<(string, Script)>>();
            CustomCallbacks = new Dictionary<string, List<(string, Script)>>();
            SharedApis = new List<SharedAPI>();

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

        public void AddSharedAPI(SharedAPI api)
        {
            SharedApis.Add(api);
        }
        public void AddType(string name, Type type)
        {
            if (CustomTypes.ContainsKey(name))
                throw new ArgumentException($"Type {name} is exists!");
            CustomTypes.Add(name, type);
        }
        public void AddDelegate(string name, Delegate del)
        {
            if (CustomDelegates.ContainsKey(name))
                throw new ArgumentException($"Delegate {name} is exists!");
            CustomDelegates.Add(name, del);
        }
        public void AddValue(string name, object val)
        {
            if (CustomValues.ContainsKey(name))
                throw new ArgumentException($"Value {name} is exists!");
            CustomValues.Add(name, val);
        }
        public void AddEvent(string name, List<(string, Script)> delegates)
        {
            if (CustomCallbacks.ContainsKey(name))
                throw new ArgumentException($"Callback {name} is exists!");
            CustomCallbacks.Add(name, delegates);
        }

        public void RemoveEvent(ScriptEvent eve)
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
            foreach (var item in SharedApis)
            {
                item.SetupAPI(this);
            }
        }

        public bool AddDelegateSafe(string name, Delegate del)
        {
            if (CustomDelegates.ContainsKey(name))
                return false;

            CustomDelegates.Add(name, del);
            return true;
        }

        public bool AddTypeSafe(string name, Type type)
        {
            if (CustomTypes.ContainsKey(name))
                return false;

            CustomTypes.Add(name, type);
            return true;
        }

        public bool AddValueSafe(string name, object obj)
        {
            if (CustomValues.ContainsKey(name))
                return false;

            CustomValues.Add(name, obj);
            return true;
        }

        public bool AddEventSafe(string name, List<(string, Script)> callbacklist)
        {
            if (CustomCallbacks.ContainsKey(name))
                return false;

            CustomCallbacks.Add(name, callbacklist);
            return true;
        }
    }
}
