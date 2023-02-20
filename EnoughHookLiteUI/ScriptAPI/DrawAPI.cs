using EnoughHookLite.Scripting.Integration;
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
using System.Runtime.InteropServices;

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

        protected override void OnSetupModule(ScriptModule module)
        {
            module.AddDelegate("getOverlayAPI", (Func<OverlayAPI>)(() => { return OverlayAPI; }));
            module.AddDelegate("getDrawingAPI", (Func<DrawingAPI>)(() => { return DrawingAPI; }));
        }

        protected override void OnSetupTypes(ISharedGlobalHandler handler)
        {
            handler.AddType("Brush", typeof(Brush));
            handler.AddType("SolidBrush", typeof(SolidBrush));
            handler.AddType("LinearGradientBrush", typeof(LinearGradientBrush));
            handler.AddType("PathGradientBrush", typeof(PathGradientBrush));
            handler.AddType("HatchBrush", typeof(HatchBrush));
            handler.AddType("TextureBrush", typeof(TextureBrush));

            handler.AddType("Color", typeof(Color));
            handler.AddType("Pen", typeof(Pen));
            handler.AddType("Font", typeof(Font));
            handler.AddType("PointF", typeof(PointF));

            handler.AddEvent("OnDraw", DrawList);
        }
    }
}
