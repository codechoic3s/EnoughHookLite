using EnoughHookLiteUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.ScriptAPI.Wraps
{
    public sealed class OverlayAPI
    {
        private Drawer Drawer;
        public OverlayAPI(Drawer d)
        {
            Drawer = d;
        }

        public ulong Width => Drawer.Width;
        public ulong Height => Drawer.Height;
        public double MesureFrameTime => Drawer.MesureFrameTime;
        public double MesureFrameRate => Drawer.MesureFrameRate;
    }
}
