using EnoughHookLite.Logging;
using EnoughHookLite.Sys;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public class ScriptHost
    {
        // native
        public ScriptRuntime ScriptRuntime;
        public ScriptEngine ScriptEngine;
        //public AppDomain IsolatedDomain; // in future

        // code
        public ScriptLoader ScriptLoader;
        private LogEntry LogScriptHost;
        internal App App;
        
        public ScriptHost(App app)
        {
            App = app;

            LogScriptHost = new LogEntry(() => { return "[ScriptHost] "; });
            App.LogHandler.AddEntry("ScriptHost", LogScriptHost);
            //IsolatedDomain = AppDomain.CreateDomain("ScriptHost");
        }

        public void Execute(Script script)
        {
            ScriptEngine.Execute(script.RawScript, script.ScriptScope);
        }
        public dynamic ExecuteDyn(Script script)
        {
            return ScriptEngine.Execute(script.RawScript, script.ScriptScope);
        }
        public T ExecuteTyped<T>(Script script)
        {
            return ScriptEngine.Execute<T>(script.RawScript, script.ScriptScope);
        }

        public void SetupHost()
        {
            LogScriptHost.Log("Initing ScriptEngine...");
            ScriptRuntime = Python.CreateRuntime();
            ScriptEngine = ScriptRuntime.GetEngine("Python");
            LogScriptHost.Log("Inited.");
        }

        public void SetupLoader(App app)
        {
            ScriptLoader = new ScriptLoader(this);
            ScriptLoader.SetupGlobalAPI();

            ScriptLoader.AllocateScripts();
            ScriptLoader.SetupScripts();
            app.BeforeSetupScript?.Invoke(app);
            

            app.SubAPI.StartAll();

            ScriptLoader.StartAll();
        }
    }
}
