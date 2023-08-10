using EnoughHookLite.Logging;
using EnoughHookLite.Pointing;
using EnoughHookLite.Scripting;
using EnoughHookLite.Sys;
using EnoughHookLite.Utilities;
using EnoughHookLite.Utilities.ClientClassManaging;
using EnoughHookLite.Utilities.Conf;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Threading;

namespace EnoughHookLite
{
    public class App
    {
        private string Title;
        //private const string LastBuild = "10/10/2021 8:00PM";
        public bool IsWorking { get; private set; }
        internal Thread MainThread;
        public SubAPI SubAPI { get; private set; }
        public ScriptHost ScriptHost { get; private set; }
        public ConfigManager ConfigManager { get; private set; }
        public DebugTools DebugTools { get; private set; }
        public static LogHandler LogHandler = new LogHandler();
        private static LogEntry LogFramework;
        public Action<App> BeforeSetupScript;
        public Action<Point, Vector2> OnUpdate;
        public bool IsForeground { get; private set; }
        public bool HandleStart(string[] args)
        {
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            LoadConfig(basedir);
            ProtectStart.Setup(args);
            if (ProtectStart.Handle(ConfigManager.Debug.Config))
            {
                Start();
                return true;
            }
            return false;
        }
        public void Start()
        {
            if (IsWorking)
                LogFramework.Log("Is current working!");
            else if (!IsWorking)
                IsWorking = true;

            LogHandler.Writer = ConsoleMessage;
            
            MainThread = new Thread(Work);
            MainThread.Start();
        }
        public void Stop()
        {
            IsWorking = false;
        }
        private void ConsoleMessage(string log)
        {
            Console.WriteLine(log);
        }
        public static void SetupCrashHandler()
        {
            LogFramework = new LogEntry(() => { return "[Framework] "; });
            LogHandler.AddEntry("Framework", LogFramework);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            Console.ForegroundColor = ConsoleColor.Red;
            
            LogFramework.Log(ExceptionHandler.HandleEception(exception, true));
            string log = LogHandler.GetAll();
            File.WriteAllText("log.txt", log);
        }
        private void Work()
        {
            Version ver = Assembly.GetEntryAssembly().GetName().Version;

            DebugTools = new DebugTools(this, ConfigManager);
            DebugTools.OnStartDebug();

            string text = "";
            text +=
                "\n/EnoughHookLite/\n" +
                "   ~ Source SDK host ~ \n" +
                "       build: " + 291 + "\n";
            LogFramework.Log(text);

            //Console.Title = Title;
            SubAPI = new SubAPI(ConfigManager.Modules.Config);
            SubAPI.ProcessFetch(ConfigManager.Engine.Config.ProcessName);
            
            if (SubAPI.ModulesFetch())
            {
                SubAPI.PointManager.InitSignatures(ConfigManager.Engine.Config);
                SubAPI.PointManager.InitClientClasses();

                DebugTools.OnDumpDebug();

                if (!SubAPI.ParseDefaultModules())
                    return;

                ScriptHost = new ScriptHost(this);
                ScriptHost.SetupHost();
                ScriptHost.SetupLoader(this);
                
                while (true)
                {
                    IsForeground = SubAPI.Process.IsForeground();
                    SubAPI.Process.UpdateWindow();
                    OnUpdate?.Invoke(SubAPI.Process.Position, SubAPI.Process.Size);
                    Thread.Sleep(1000);
                }
            }
            LogFramework.Log("End...");
            Console.ReadKey();
        }
        private void LoadConfig(string basedir)
        {
            LogFramework.Log("Loading config...");
            ConfigManager = new ConfigManager(basedir);
            ConfigManager.Load();
        }
    }
}
