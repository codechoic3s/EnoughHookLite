using EnoughHookLite.Logging;
using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace EnoughHookLite.Scripting
{
    public class ScriptHost
    {
        public ScriptOptions SOptions { get; private set; }

        // code
        public ScriptLoader ScriptLoader;
        private LogEntry LogScriptHost;
        internal App App;
        
        public ScriptHost(App app)
        {
            App = app;

            LogScriptHost = new LogEntry(() => { return "[ScriptHost] "; });
            App.LogHandler.AddEntry("ScriptHost", LogScriptHost);
        }
        public Microsoft.CodeAnalysis.Scripting.Script CreateScope(Script real)
        {
            return CSharpScript.Create(real.RawScript, ScriptLoader.ScriptApi.SOptions);
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