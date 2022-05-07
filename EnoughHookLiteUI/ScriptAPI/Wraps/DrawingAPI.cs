using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.ScriptAPI.Wraps
{
    public sealed class DrawingAPI
    {
        private DrawAPI DrawAPI;
        public DrawingAPI(DrawAPI dapi)
        {
            DrawAPI = dapi;
        }
        public void DrawString(string text, Font font, Brush brush, double x, double y)
        {
            DrawAPI.GFX.DrawString(text, font, brush, (float)x, (float)y);
        }
        public void DrawRectangle(Pen pen, double x, double y, double w, double h)
        {
            DrawAPI.GFX.DrawRectangle(pen, (float)x, (float)y, (float)w, (float)h);
        }
        public void FillRectangle(Brush brush, double x, double y, double w, double h)
        {
            DrawAPI.GFX.FillRectangle(brush, (float)x, (float)y, (float)w, (float)h);
        }
        public void DrawEllipse(Pen pen, double x, double y, double w, double h)
        {
            DrawAPI.GFX.DrawEllipse(pen, (float)x, (float)y, (float)w, (float)h);
        }
        public void FillEllipse(Brush brush, double x, double y, double w, double h)
        {
            DrawAPI.GFX.FillEllipse(brush, (float)x, (float)y, (float)w, (float)h);
        }
        public void DrawLines(Pen pen, PointF[] points)
        {
            DrawAPI.GFX.DrawLines(pen, points);
        }
    }
}
