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
            app.Start(args);
            return app;
        }
    }
}
