using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite
{
    public class Program
    {
        static void Main(string[] args)
        {
            Start(args);
        }

        public static App Start(string[] args)
        {
            var app = new App();
            if (args.Length == 2)
            {
                app.ChangeName = true;
                app.ChangeableName = args[0];
                app.RemoveName = args[1];
            }
            app.Start();
            return app;
        }
    }
}
