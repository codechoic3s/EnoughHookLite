using EnoughHookLite.Scripting;
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
        public DrawAPI(Graphics gfx, Drawer drawer, List<(string, Script)> callbacklist)
        {
            GFX = gfx;
            DrawList = callbacklist;
            Drawer = drawer;
        }

        public override void OnSetupAPI(ISharedHandler local)
        {
            local.AddDelegate("getDrawer", (Func<Drawer>)(() => { return Drawer; }));

            local.AddType("Brush", typeof(Brush));
            local.AddType("SolidBrush", typeof(SolidBrush));
            local.AddType("Color", typeof(Color));
            local.AddType("Pen", typeof(Pen));
            local.AddType("Font", typeof(Font));
            local.AddType("PointF", typeof(PointF));

            local.AddDelegate("drawString", (Action<string, Font, Brush, float, float>)GFX.DrawString);
            local.AddDelegate("drawLines", (Action<Pen, PointF[]>)GFX.DrawLines);

            local.AddDelegate("drawRectangle", (Action<Pen, float, float, float, float>)GFX.DrawRectangle);
            local.AddDelegate("fillRectangle", (Action<Brush, float, float, float, float>)GFX.FillRectangle);

            local.AddDelegate("drawEllipse", (Action<Pen, float, float, float, float>)GFX.DrawEllipse);
            local.AddDelegate("fillEllipse", (Action<Brush, float, float, float, float>)GFX.FillEllipse);

            local.AddDelegate("drawCurve", (Action<Pen, PointF[]>)GFX.DrawCurve);
            local.AddDelegate("drawClosedCurve", (Action<Pen, PointF[]>)GFX.DrawClosedCurve);
            local.AddDelegate("fillClosedCurve", (Action<Brush, PointF[]>)GFX.FillClosedCurve);

            local.AddEvent("OnDraw", DrawList);
        }
    }
}
