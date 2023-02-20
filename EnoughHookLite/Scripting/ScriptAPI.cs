using EnoughHookLite.Scripting.Integration.Apis;
using EnoughHookLite.Scripting.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using EnoughHookLite.Sys;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using EnoughHookLite.Logging;

namespace EnoughHookLite.Scripting
{
    /// <summary>
    /// Global class of initialization of customs & events.
    /// </summary>
    public sealed class ScriptAPI : ISharedGlobalHandler, ISharedLoader
    {
        internal Dictionary<string, List<(string, Script)>> Callbacks;
        internal Dictionary<string, List<(string, Script)>> CustomCallbacks;

        private Dictionary<string, Type> CustomTypes;
        //private Dictionary<string, Delegate> CustomDelegates;
        //private Dictionary<string, object> CustomValues;

        internal Dictionary<string, object> GlobalValues;
        internal Dictionary<string, SharedAPI> SharedApis;

        internal ScriptHost ScriptHost;

        private LogEntry LogScriptAPI;

        public ScriptAPI(ScriptHost host)
        {
            ScriptHost = host;
            
            Callbacks = new Dictionary<string, List<(string, Script)>>();
            CustomCallbacks = new Dictionary<string, List<(string, Script)>>();
            SharedApis = new Dictionary<string, SharedAPI>();

            CustomTypes = new Dictionary<string, Type>();
            //CustomDelegates = new Dictionary<string, Delegate>();
            //CustomValues = new Dictionary<string, object>();

            GlobalValues = new Dictionary<string, object>();
        }

        public void Setup()
        {
            LogScriptAPI = new LogEntry(() => { return "[ScriptAPI] "; });
            App.LogHandler.AddEntry("ScriptAPI", LogScriptAPI);

            LogScriptAPI.Log($"Allocating APIs...");

            AddSharedAPI("offsets", new OffsetsAPI(ScriptHost.App.SubAPI.PointManager));
            AddSharedAPI("process", new ProcessEnvironmentAPI(ScriptHost.App.SubAPI));
            AddSharedAPI("source", new SourceEngineAPI(ScriptHost.App.SubAPI.Client, ScriptHost.App.SubAPI.Engine));
            AddSharedAPI("input", new InputAPI(ScriptHost.App.SubAPI.Process));
            AddSharedAPI("numerics", new NumericsAPI());
            AddSharedAPI("camera", new CameraAPI(ScriptHost.App.SubAPI.Client.Camera));

            LogScriptAPI.Log($"Allocated {SharedApis.Count} APIs.");
        }

        public void ClearAllCustom()
        {
            CustomTypes.Clear();
            //CustomDelegates.Clear();
            //CustomValues.Clear();
        }

        public void AddSharedAPI(string name, SharedAPI api)
        {
            SharedApis.Add(name, api);
        }
        
        public void AddType(string name, Type type)
        {
            if (CustomTypes.ContainsKey(name))
                throw new ArgumentException($"Type {name} is exists!");
            CustomTypes.Add(name, type);
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
        public void LoadGlobalAPI()
        {
            LogScriptAPI.Log($"Injecting {SharedApis.Count} APIs...");
            foreach (var item in SharedApis)
            {
                item.Value.SetupTypes(this);
                item.Value.SetupModule(new ScriptModule(ScriptHost.ScriptEngine, item.Key));
            }
            foreach (var item in CustomTypes)
            {
                ScriptHost.ScriptRuntime.LoadAssembly(Assembly.GetAssembly(item.Value));
            }

            LogScriptAPI.Log($"Injected {SharedApis.Count} APIs.");
        }
        public bool AddTypeSafe(string name, Type type)
        {
            if (CustomTypes.ContainsKey(name))
                return false;

            CustomTypes.Add(name, type);
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
