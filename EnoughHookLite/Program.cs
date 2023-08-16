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
           
            
            App.SetupCrashHandler();
            Start(args, out App app);
        }

        public static bool Start(string[] args, out App app)
        {
            app = new App();
            return app.HandleStart(args);
        }
    }
}
