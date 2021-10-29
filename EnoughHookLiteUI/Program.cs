using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = EnoughHookLite.Program.Start(args);

            var visual = Visualization.Create();
            visual.InitDevice();
            VisualFeatures vf = new VisualFeatures(app, visual);
            vf.Start();
        }
    }
}
