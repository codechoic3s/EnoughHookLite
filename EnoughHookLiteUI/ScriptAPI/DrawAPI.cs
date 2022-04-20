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

            local.AddDelegate("drawString", (Action<string, Font, Brush, double, double>)DrawString);
            local.AddDelegate("drawLines", (Action<Pen, PointF[]>)GFX.DrawLines);

            local.AddDelegate("drawRectangle", (Action<Pen, double, double, double, double>)DrawRectangle);
            local.AddDelegate("fillRectangle", (Action<Brush, double, double, double, double>)FillRectangle);

            local.AddDelegate("drawEllipse", (Action<Pen, double, double, double, double>)DrawEllipse);
            local.AddDelegate("fillEllipse", (Action<Brush, double, double, double, double>)FillEllipse);

            local.AddDelegate("drawCurve", (Action<Pen, PointF[]>)GFX.DrawCurve);
            local.AddDelegate("drawClosedCurve", (Action<Pen, PointF[]>)GFX.DrawClosedCurve);
            local.AddDelegate("fillClosedCurve", (Action<Brush, PointF[]>)GFX.FillClosedCurve);

            local.AddEvent("OnDraw", DrawList);
        }

        private void DrawString(string text, Font font, Brush brush, double x, double y)
        {
            GFX.DrawString(text, font, brush, (float)x, (float)y);
        }
        private void DrawRectangle(Pen pen, double x, double y, double w, double h)
        {
            GFX.DrawRectangle(pen, (float)x, (float)y, (float)w, (float)h);
        }
        private void FillRectangle(Brush brush, double x, double y, double w, double h)
        {
            GFX.FillRectangle(brush, (float)x, (float)y, (float)w, (float)h);
        }
        private void DrawEllipse(Pen pen, double x, double y, double w, double h)
        {
            GFX.DrawEllipse(pen, (float)x, (float)y, (float)w, (float)h);
        }
        private void FillEllipse(Brush brush, double x, double y, double w, double h)
        {
            GFX.FillEllipse(brush, (float)x, (float)y, (float)w, (float)h);
        }
    }
}
