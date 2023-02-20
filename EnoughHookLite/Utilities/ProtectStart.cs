using EnoughHookLite.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public static class ProtectStart
    {
        internal static bool ChangeName { get; private set; }
        internal static string ChangeableName { get; private set; }
        internal static string RemoveName { get; private set; }
        public static bool ProtectedStart { get; private set; } = Init();
        public static Action<string> SetName;

        private static LogEntry LogProtectStart;
        private static bool Init()
        {
            LogProtectStart = new LogEntry(() => { return "[protect_start] "; });
            App.LogHandler.AddEntry("ProtectStart", LogProtectStart);
            return true;
        }
        public static void Setup(string[] args)
        {
            if (args.Length == 2)
            {
                ChangeName = true;
                ChangeableName = args[0];
                RemoveName = args[1];
            }
        }
        public static bool Handle()
        {
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            if (ChangeName || !ProtectedStart)
            {
                if (ChangeName)
                {
                    SetName?.Invoke(ChangeableName);
                    LogProtectStart.Log($"Trying remove {RemoveName}");
                    File.Delete(RemoveName);
                    LogProtectStart.Log("Removed");
                }
                else if (!ProtectedStart)
                {
                    SetName?.Invoke(RandomText());
                }
                return true;
            }
            else
            {
                var name = RandomText();
                SetName?.Invoke(name);
                string minpath = basedir + @"/" + name + ".exe";
                string thispath = basedir + @"/" + AppDomain.CurrentDomain.FriendlyName;
                byte[] bytes = File.ReadAllBytes(thispath);
                File.WriteAllBytes(minpath, bytes);
                Process.Start(new ProcessStartInfo() { FileName = minpath, Arguments = $"{name} {thispath}" });
                return false;
            }
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
                    data[i] = (byte)rand1.Next(65, 90);
                else if (type == 1)
                    data[i] = (byte)rand1.Next(97, 122);
                else if (type == 2)
                    data[i] = (byte)rand1.Next(48, 57);
            }

            char[] chars = new char[size];
            for (var i = 0; i < size; i++)
                chars[i] = (char)data[i];

            return new string(chars);
        }
    }
}
