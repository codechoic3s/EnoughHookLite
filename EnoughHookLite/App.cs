using EnoughHookLite.Features;
using EnoughHookLite.Modules;
using EnoughHookLite.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Process = EnoughHookLite.Sys.Process;

namespace EnoughHookLite
{
    public class App
    {
        private string Title;
        //private const string LastBuild = "10/10/2021 8:00PM";

        public bool ProtectStart;

        public Thread MainThread;

        public Process Process;
        public Client Client;
        public Engine Engine;

        public CrosshairTrigger CrosshairTrigger;
        public MagnetTrigger MagnetTrigger;
        public BunnyHop BunnyHop;

        public ConfigManager ConfigManager;

        public bool CanNext { get; private set; }

        internal bool ChangeName;
        internal string ChangeableName;
        internal string RemoveName;

        public void Start()
        {
            ProtectStart = false;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            MainThread = new Thread(new ParameterizedThreadStart(Work));
            MainThread.Start();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exception);
            File.WriteAllText("log.txt", exception.StackTrace);
            Console.WriteLine("Log saved as log.txt");
        }

        private void Work(object obj)
        {
            if (ChangeName || !ProtectStart)
            {
                if (ChangeName)
                {
                    Title = ChangeableName;
                    File.Delete(RemoveName);
                }
                else if (!ProtectStart)
                {
                    Title = RandomText();
                }

                var ver = Assembly.GetEntryAssembly().GetName().Version;

                string text = "";
                text += "\n/EnoughHookLite/\n" + "build: " + ver.Build + "\n\n";
                text += "Include features:\n";
                text += "   1. Trigger (Crosshair).\n";
                text += "   2. Bunnyhop.\n";
                Console.WriteLine(text);

                Console.Title = Title;

                ConfigManager = new ConfigManager(AppDomain.CurrentDomain.BaseDirectory);

                Console.WriteLine("Waiting process...");

                while (Process is null)
                {
                    Process = Process.FindProcess("csgo");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Process finded!");

                Process.AllocateHandles();

                var clm = Process.GetModule("client.dll", out bool cf);
                var em = Process.GetModule("engine.dll", out bool ef);

                if (!cf)
                {
                    Console.WriteLine("Not founded client.dll");
                }
                else if (!ef)
                {
                    Console.WriteLine("Not founded engine.dll");
                }
                else
                {
                    Client = new Client(clm, this);
                    Engine = new Engine(em, this);

                    Engine.Start();
                    Client.Start();

                    CrosshairTrigger = new CrosshairTrigger(this);
                    MagnetTrigger = new MagnetTrigger(this);
                    BunnyHop = new BunnyHop(this);

                    //CrosshairTrigger.Start();
                    MagnetTrigger.Start();
                    BunnyHop.Start();

                    while (true)
                    {
                        CanNext = Process.IsForeground();
                        Thread.Sleep(1000);
                    }
                }
                Console.WriteLine("End...");
                Console.ReadKey();
            }
            else
            {
                Title = RandomText();
                var minpath = Title + ".exe";
                var thispath = AppDomain.CurrentDomain.FriendlyName;
                var bytes = File.ReadAllBytes(thispath);
                File.WriteAllBytes(minpath, bytes);
                System.Diagnostics.Process.Start(new ProcessStartInfo() { FileName = minpath, Arguments = $"{Title} {thispath}" });
                Environment.Exit(0);
            }
        }

        private static string RandomText()
        {
            var rand = new Random();

            int size = rand.Next(5, 20);
            byte[] data = new byte[size];

            var rand1 = new Random(rand.Next());

            for (var i = 0; i < size; i++)
            {
                rand1 = new Random(rand1.Next());
                var type = rand1.Next(0, 3);
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
