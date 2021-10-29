using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI
{
    public class RenderInfo
    {
        private Stopwatch Mesure;
        public double FrameTime { get; private set; }
        public ulong FrameRate { get; private set; }

        public void Begin()
        {
            Mesure.Restart();

        }
        public void End()
        {
            Mesure.Stop();
            FrameTime = Mesure.Elapsed.TotalMilliseconds;
            FrameRate = (ulong)(1000.0 / FrameTime);
        }
    }
}
