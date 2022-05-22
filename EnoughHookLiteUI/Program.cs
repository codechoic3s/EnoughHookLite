using EnoughHookLite;
using EnoughHookLiteUI.Utils;
using EnoughHookLiteUI.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLiteUI
{
    class Program
    {
        static Thread WindowThread;
        static EHLWindow Window;

        static void Main(string[] args)
        {
            App.SetupCrashHandler();
            App app = new App();
            Start(app, args);
        }

        private static void Start(App app, string[] args)
        {
            WindowThread = new Thread(WWork);
            WindowThread.SetApartmentState(ApartmentState.STA);
            WindowThread.Start(new object[] {app, args});
        }

        private static void WWork(object arg)
        {
            var objs = (object[])arg;

            var app = (App)objs[0];
            var args = (string[])objs[1];

            if (!app.HandleStart(args))
                return;

            Window = new EHLWindow(app, args);
            Window.ShowDialog();
        }
    }
}
