using EnoughHookLite.Features;
using EnoughHookLite.Modules;
using EnoughHookLite.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public Thread MainThread;

        public Process Process;
        public Client Client;
        public Engine Engine;

        public Trigger Trigger;
        public BunnyHop BunnyHop;

        public ConfigManager ConfigManager;

        public void Start()
        {
            MainThread = new Thread(new ThreadStart(Work));
            MainThread.Start();
        }

        private void Work()
        {
            Title = RandomText();
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

            Client = new Client(Process.GetModule("client.dll"), this);
            Engine = new Engine(Process.GetModule("engine.dll"), this);

            Client.Start();

            Trigger = new Trigger(this);
            BunnyHop = new BunnyHop(this);

            Trigger.Start();
            BunnyHop.Start();
        }

        private static string RandomText()
        {
            var rand = new Random();
            int size = rand.Next(5, 20);
            byte[] data = new byte[size];
            rand.NextBytes(data);

            char[] chars = new char[size];
            for (var i = 0; i < size; i++)
            {
                chars[i] = (char)data[i];
            }

            return new string(chars);
        }
    }
}
