using EnoughHookLite.Utils;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI
{
    public static class GFXTools
    {
        /// <summary>
        /// Check if vector is valid to draw in screen space.
        /// </summary>
        public static bool IsValidScreen(this Microsoft.DirectX.Vector3 value)
        {
            return !value.X.IsInfinityOrNaN() && !value.Y.IsInfinityOrNaN() && value.Z >= 0 && value.Z < 1;
        }

        /// <summary>
        /// Draw 2D polyline in screen space.
        /// </summary>
        public static void DrawPolyline(this Device device, Vector3[] vertices, Color color)
        {
            if (vertices.Length < 2 || vertices.Any(v => !v.IsValidScreen()))
            {
                return;
            }

            var vertexStreamZeroData = vertices.Select(v => new CustomVertex.TransformedColored(v.X, v.Y, v.Z, 0, color.ToArgb())).ToArray();
            device.VertexFormat = VertexFormats.Diffuse | VertexFormats.Transformed;
            device.DrawUserPrimitives(PrimitiveType.LineStrip, vertexStreamZeroData.Length - 1, vertexStreamZeroData);
        }
    }
}
