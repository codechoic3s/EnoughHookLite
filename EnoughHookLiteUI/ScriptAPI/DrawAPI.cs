using EnoughHookLite.Scripting;
using EnoughHookLiteUI.ScriptAPI.Wraps;
using EnoughHookLiteUI.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.ScriptAPI
{
    public sealed class DrawAPI : SharedAPI
    {
        internal Graphics GFX;
        private List<(string, Script)> DrawList;
        private Drawer Drawer;
        private DrawingAPI DrawingAPI;
        public DrawAPI(Graphics gfx, Drawer drawer, List<(string, Script)> callbacklist)
        {
            GFX = gfx;
            DrawList = callbacklist;
            Drawer = drawer;
            DrawingAPI = new DrawingAPI(this);
        }

        public override void OnSetupAPI(ISharedHandler local)
        {
            local.AddDelegate("getDrawer", (Func<Drawer>)(() => { return Drawer; }));
            local.AddDelegate("getDrawingAPI", (Func<DrawingAPI>)(() => { return DrawingAPI; }));

            local.AddType("Brush", typeof(Brush));
            local.AddType("SolidBrush", typeof(SolidBrush));
            local.AddType("Color", typeof(Color));
            local.AddType("Pen", typeof(Pen));
            local.AddType("Font", typeof(Font));
            local.AddType("PointF", typeof(PointF));

            local.AddEvent("OnDraw", DrawList);
        }
    }
}
