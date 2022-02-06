using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.API
{
    public sealed class DrawAPI
    {
        internal Graphics GFX;

        public DrawAPI(Graphics gfx)
        {
            GFX = gfx;
        }

        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            GFX.DrawString(s, font, brush, x, y);
        }
        public void DrawLines(Pen pen, params PointF[] points)
        {
            GFX.DrawLines(pen, points);
        }

        public void DrawRectangle(Pen pen, float x, float y, float w, float h)
        {
            GFX.DrawRectangle(pen, x, y, w, h);
        }
        public void FillRectangle(Brush brush, float x, float y, float w, float h)
        {
            GFX.FillRectangle(brush, x, y, w, h);
        }
        
        public void DrawEllipse(Pen pen, float x, float y, float w, float h)
        {
            GFX.DrawEllipse(pen, x, y, w, h);
        }
        public void FillEllipse(Brush brush, float x, float y, float w, float h)
        {
            GFX.FillEllipse(brush, x, y, w, h);
        }

        public void DrawCurve(Pen pen, params PointF[] points)
        {
            GFX.DrawCurve(pen, points);
        }

        public void DrawClosedCurve(Pen pen, params PointF[] points)
        {
            GFX.DrawClosedCurve(pen, points);
        }
        public void FillClosedCurve(Brush brush, params PointF[] points)
        {
            GFX.FillClosedCurve(brush, points);
        }
    }
}
