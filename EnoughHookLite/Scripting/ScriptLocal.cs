using EnoughHookLite.Scripting.Integration.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public sealed class ScriptLocal
    {
        //private Dictionary<string, Type> Types;
        private Dictionary<string, Delegate> Delegates;
        private Dictionary<string, object> Values;

        private ScriptAPI ScriptApi;
        private Script Script;

        private Dictionary<string, object> LocalValues;

        public ScriptLocal(Script script, ScriptAPI api)
        {
            ScriptApi = api;
            Script = script;
            Delegates = new Dictionary<string, Delegate>();
            Values = new Dictionary<string, object>();
            //Types = new Dictionary<string, Type>();
            LocalValues = new Dictionary<string, object>();
        }

        private void SetupSystemAPI()
        {
            Delegates.Add("twait", (Action<int>)Thread.Sleep);
            Delegates.Add("print", (Action<string>)Script.LogScript.Log);
            Delegates.Add("printo", (Action<object>)Script.LogScript.Log);
        }
        private void SetupConfigAPI()
        {
            Values.Add("config", Script.Config);
            Delegates.Add("configSync", (Action)Script.SyncConfig);
        }
        private void SetupCallbackAPI()
        {
            Delegates.Add("cbadd", (ScriptDelegate)Script.OnNewEvent);
            Delegates.Add("cbrem", (Action<string, string>)Script.OnRemEvent);
        }
        private void SetupValueDefinition()
        {
            Delegates.Add("LocalAdd", (Action<string, object>)OnAddLocalValue);
            Delegates.Add("LocalDel", (Action<string>)OnDelLocalValue);

            Delegates.Add("GlobalAdd", (Action<string, object>)OnAddGlobalValue);
            Delegates.Add("GlobalDel", (Action<string>)OnDelGlobalValue);
        }

        private void OnDelLocalValue(string name)
        {
            Script.ScriptScope.SetVariable(name, (object)null);
            LocalValues.Remove(name);
        }
        private void OnAddLocalValue(string name, object value)
        {
            if (!LocalValues.ContainsKey(name))
                LocalValues.Add(name, value);

            Script.ScriptScope.SetVariable(name, value);
        }

        private void OnDelGlobalValue(string name)
        {
            Script.ScriptScope.SetVariable(name, (object)null);
            ScriptApi.GlobalValues.Remove(name);
        }
        private void OnAddGlobalValue(string name, object value)
        {
            if (!ScriptApi.GlobalValues.ContainsKey(name))
                ScriptApi.GlobalValues.Add(name, value);

            Script.ScriptScope.SetVariable(name, value);
        }
        public void SetupDefaultAPI()
        {
            SetupSystemAPI();
            SetupConfigAPI();
            SetupCallbackAPI();
            SetupValueDefinition();
        }

        public void LoadAPI()
        {
            /*
            foreach (var item in Types)
            {
                //Script.ScriptScope.SetVariable(item.Key, TypeReference.CreateTypeReference(Script.JSEngine, item.Value));
                Script.ScriptScope.CreateObjRef(item.Value);
            }*/
            foreach (var item in Delegates)
            {
                Script.ScriptScope.SetVariable(item.Key, item.Value);
            }
            foreach (var item in Values)
            {
                Script.ScriptScope.SetVariable(item.Key, (dynamic)item.Value);
            }
        }
    }
}
