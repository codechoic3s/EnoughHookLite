﻿using Jint.Runtime.Interop;
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
        private Dictionary<string, Type> Types;
        private Dictionary<string, Delegate> Delegates;
        private Dictionary<string, object> Values;

        private ScriptAPI JSApi;
        private Script Script;

        private Dictionary<string, object> LocalValues;

        public ScriptLocal(Script script, ScriptAPI api)
        {
            JSApi = api;
            Script = script;
            Delegates = new Dictionary<string, Delegate>();
            Values = new Dictionary<string, object>();
            Types = new Dictionary<string, Type>();
            LocalValues = new Dictionary<string, object>();
        }

        private void SetupTypes()
        {
            Types.Add("VK", typeof(Sys.VK));
        }
        private void SetupSystemAPI()
        {
            Delegates.Add("twait", (Action<int>)Thread.Sleep);
            Delegates.Add("log", (Action<string>)Script.LogIt);
        }
        private void SetupConfigAPI()
        {
            Values.Add("config", Script.Config);
            Delegates.Add("sync_config", (Action)Script.SyncConfig);
        }
        private void SetupCallbackAPI()
        {
            Delegates.Add("cbadd", (ScriptDelegate)Script.OnNewEvent);
        }
        private void SetupValueDefinition()
        {
            Delegates.Add("LocalAdd", (Action<string, object>)OnAddLocalValue);
            Delegates.Add("LocalDel", (Action<string>)OnDelLocalValue);

            Delegates.Add("GlobalAdd", (Action<string, object>)OnAddGlobalValue);
            Delegates.Add("GlobalDel", (Action<string>)OnDelGlobalValue);
        }
        private void SetupAPI()
        {
            var app = Script.Loader.App;
            Delegates.Add("getSubAPI", (Func<SubAPI>)(() => { return app.SubAPI; }));
            Delegates.Add("getIsForeground", (Func<bool>)(() => { return app.IsForeground; }));
        }

        private void OnDelLocalValue(string name)
        {
            Script.JSEngine.SetValue(name, (object)null);
            LocalValues.Remove(name);
        }
        private void OnAddLocalValue(string name, object value)
        {
            if (!LocalValues.ContainsKey(name))
                LocalValues.Add(name, value);

            Script.JSEngine.SetValue(name, value);
        }

        private void OnDelGlobalValue(string name)
        {
            Script.JSEngine.SetValue(name, (object)null);
            JSApi.GlobalValues.Remove(name);
        }
        private void OnAddGlobalValue(string name, object value)
        {
            if (!JSApi.GlobalValues.ContainsKey(name))
                JSApi.GlobalValues.Add(name, value);

            Script.JSEngine.SetValue(name, value);
        }

        public void SetupDefaultAPI()
        {
            SetupSystemAPI();
            SetupConfigAPI();
            SetupTypes();
            SetupCallbackAPI();
            SetupValueDefinition();
            SetupAPI();
        }

        public void LoadAPI()
        {
            foreach (var item in Types)
            {
                Script.JSEngine.SetValue(item.Key, TypeReference.CreateTypeReference(Script.JSEngine, item.Value));
            }
            foreach (var item in Delegates)
            {
                Script.JSEngine.SetValue(item.Key, item.Value);
            }
            foreach (var item in Values)
            {
                Script.JSEngine.SetValue(item.Key, (dynamic)item.Value);
            }
            JSApi.LoadAPI(Script);
        }
    }
}