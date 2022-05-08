using EnoughHookLite.Scripting;
using EnoughHookLiteUI.ScriptAPI.Wraps;
using EnoughHookLiteUI.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private OverlayAPI OverlayAPI;
        public DrawAPI(Graphics gfx, Drawer drawer, List<(string, Script)> callbacklist)
        {
            GFX = gfx;
            DrawList = callbacklist;
            Drawer = drawer;
            DrawingAPI = new DrawingAPI(this);
            OverlayAPI = new OverlayAPI(Drawer);
        }

        public override void OnSetupAPI(ISharedHandler local)
        {
            local.AddDelegate("getOverlayAPI", (Func<OverlayAPI>)(() => { return OverlayAPI; }));
            local.AddDelegate("getDrawingAPI", (Func<DrawingAPI>)(() => { return DrawingAPI; }));

            local.AddType("Brush", typeof(Brush));
            local.AddType("SolidBrush", typeof(SolidBrush));
            local.AddType("LinearGradientBrush", typeof(LinearGradientBrush));
            local.AddType("PathGradientBrush", typeof(PathGradientBrush));
            local.AddType("HatchBrush", typeof(HatchBrush));
            local.AddType("TextureBrush", typeof(TextureBrush));

            local.AddType("Color", typeof(Color));
            local.AddType("Pen", typeof(Pen));
            local.AddType("Font", typeof(Font));
            local.AddType("PointF", typeof(PointF));

            local.AddEvent("OnDraw", DrawList);
        }
    }
}
