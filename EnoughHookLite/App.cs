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

        public bool ProtectStart { get; private set; }
        public bool IsWorking { get; private set; }

        internal Thread MainThread;

        public SubAPI SubAPI { get; private set; }

        public static Log Log = new Log();

        public ScriptLoader JSLoader { get; private set; }
        public ConfigManager ConfigManager { get; private set; }
        public DebugTools DebugTools { get; private set; }

        public Action<App> BeforeSetupScript;
        public Action<Point, Vector2> OnUpdate;

        public bool IsForeground { get; private set; }

        internal bool ChangeName;
        internal string ChangeableName;
        internal string RemoveName;

        private void LogIt(string log, bool withaction = true)
        {
            Log.LogIt("[Framework] " + log, withaction);
        }

        public void Start(string[] args)
        {
            if (args.Length == 2)
            {
                ChangeName = true;
                ChangeableName = args[0];
                RemoveName = args[1];
            }

            if (IsWorking)
                LogIt("Is current working!");
            else if (!IsWorking)
                IsWorking = true;
            ProtectStart = false;
/*
#if DEBUG
            ProtectStart = false;
#else
            ProtectStart = true;
#endif
*/

            Log.LogAction = ConsoleMessage;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            MainThread = new Thread(Work);
            MainThread.Start();
        }

        private void ConsoleMessage(string log)
        {
            Console.WriteLine(log);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            Console.ForegroundColor = ConsoleColor.Red;
            LogIt(exception.ToString());
            string log = Log.Get();
            File.WriteAllText("log.txt", log);
        }

        private void Work()
        {
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            if (ChangeName || !ProtectStart)
            {
                if (ChangeName)
                {
                    Title = ChangeableName;
                    LogIt($"Trying remove {RemoveName}", false);
                    File.Delete(RemoveName);
                    LogIt("Removed", false);
                }
                else if (!ProtectStart)
                {
                    Title = RandomText();
                }

                Version ver = Assembly.GetEntryAssembly().GetName().Version;

                string text = "";
                text += 
                    "\n/EnoughHookLite/\n" +
                    "   ~ Source SDK js host ~ \n" +
                    "       build: " + ver.Build + "\n";
                //text += "Include features:\n";
                //text += "   1. Trigger.\n";
                //text += "   2. Bunnyhop.\n";
                LogIt(text);

                LoadConfig(basedir);
                //Console.Title = Title;
                SubAPI = new SubAPI(ConfigManager.Modules.Config);
                SubAPI.ProcessFetch(ConfigManager.Engine.Config.ProcessName);

                DebugTools = new DebugTools(SubAPI, ConfigManager);

                if (SubAPI.ModulesFetch())
                {
                    SubAPI.PointManager.InitSignatures(ConfigManager.Engine.Config);
                    SubAPI.PointManager.InitClientClasses();

                    DebugTools.OnStartDebug();

                    if (!SubAPI.ParseDefaultModules())
                        return;

                    JSLoader = new ScriptLoader(this);

                    JSLoader.AllocateScripts();
                    BeforeSetupScript?.Invoke(this);
                    JSLoader.SetupAll();

                    SubAPI.StartAll();

                    JSLoader.StartAll();

                    while (true)
                    {
                        IsForeground = SubAPI.Process.IsForeground();
                        SubAPI.Process.UpdateWindow();
                        OnUpdate?.Invoke(SubAPI.Process.Position, SubAPI.Process.Size);
                        Thread.Sleep(1000);
                    }
                }
                LogIt("End...");
                Console.ReadKey();
            }
            else
            {
                Title = RandomText();
                string minpath = basedir + @"/" + Title + ".exe";
                string thispath = basedir + @"/" + AppDomain.CurrentDomain.FriendlyName;
                byte[] bytes = File.ReadAllBytes(thispath);
                File.WriteAllBytes(minpath, bytes);
                System.Diagnostics.Process.Start(new ProcessStartInfo() { FileName = minpath, Arguments = $"{Title} {thispath}" });
                Environment.Exit(0);
            }
        }

        private void LoadConfig(string basedir)
        {
            LogIt("Loading config...");
            ConfigManager = new ConfigManager(basedir);
            ConfigManager.Load();
        }

        public static string RandomText()
        {
            Random rand = new Random();

            int size = rand.Next(5, 20);
            byte[] data = new byte[size];

            Random rand1 = new Random(rand.Next());

            for (var i = 0; i < size; i++)
            {
                rand1 = new Random(rand1.Next());
                int type = rand1.Next(0, 3);
                if (type == 0)
                {
                    data[i] = (byte)rand1.Next(65, 90);
                }
                else if (type == 1)
                {
                    data[i] = (byte)rand1.Next(97, 122);
                }
                else if (type == 2)
                {
                    data[i] = (byte)rand1.Next(48, 57);
                }
            }

            char[] chars = new char[size];
            for (var i = 0; i < size; i++)
            {
                chars[i] = (char)data[i];
            }

            return new string(chars);
        }
    }
}
