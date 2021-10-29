using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utils
{
    public static class Math
    {
        private const double _PI_Over_180 = System.Math.PI / 180.0;

        private const double _180_Over_PI = 180.0 / System.Math.PI;

        public static double DegreeToRadian(this double degree)
        {
            return degree * _PI_Over_180;
        }

        public static double RadianToDegree(this double radian)
        {
            return radian * _180_Over_PI;
        }

        public static float DegreeToRadian(this float degree)
        {
            return (float)(degree * _PI_Over_180);
        }

        public static float RadianToDegree(this float radian)
        {
            return (float)(radian * _180_Over_PI);
        }

        /// <summary>
        /// Get viewport matrix.
        /// </summary>
        public static Matrix GetMatrixViewport(Vector2 screenSize)
        {
            return GetMatrixViewport(new Viewport
            {
                X = 0,
                Y = 0,
                Width = screenSize.X,
                Height = screenSize.Y,
                MinZ = 0,
                MaxZ = 1,
            });
        }

        /// <summary>
        /// Get viewport matrix.
        /// </summary>
        public static Matrix GetMatrixViewport(in Viewport viewport)
        {
            return new Matrix
            {
                M11 = viewport.Width * 0.5f,
                M12 = 0,
                M13 = 0,
                M14 = 0,

                M21 = 0,
                M22 = -viewport.Height * 0.5f,
                M23 = 0,
                M24 = 0,

                M31 = 0,
                M32 = 0,
                M33 = viewport.MaxZ - viewport.MinZ,
                M34 = 0,

                M41 = viewport.X + viewport.Width * 0.5f,
                M42 = viewport.Y + viewport.Height * 0.5f,
                M43 = viewport.MinZ,
                M44 = 1
            };
        }

        /// <summary>
        /// Transform value.
        /// </summary>
        public static Vector3 Transform(this in Matrix matrix, Vector3 value)
        {
            var wInv = 1.0 / ((double)matrix.M14 * (double)value.X + (double)matrix.M24 * (double)value.Y + (double)matrix.M34 * (double)value.Z + (double)matrix.M44);
            return new Vector3
            (
                (float)(((double)matrix.M11 * (double)value.X + (double)matrix.M21 * (double)value.Y + (double)matrix.M31 * (double)value.Z + (double)matrix.M41) * wInv),
                (float)(((double)matrix.M12 * (double)value.X + (double)matrix.M22 * (double)value.Y + (double)matrix.M32 * (double)value.Z + (double)matrix.M42) * wInv),
                (float)(((double)matrix.M13 * (double)value.X + (double)matrix.M23 * (double)value.Y + (double)matrix.M33 * (double)value.Z + (double)matrix.M43) * wInv)
            );
        }

        public static float AngleTo(this Vector3 vector, Vector3 other)
        {
            return (float)System.Math.Acos(vector.Normalized().Dot(other.Normalized()));
        }
        /// <inheritdoc cref="Vector3.Normalize(Vector3)"/>
        public static Vector3 Normalized(this Vector3 value)
        {
            return Vector3.Normalize(value);
        }

        /// <inheritdoc cref="Vector3.Cross"/>
        public static Vector3 Cross(this Vector3 left, Vector3 right)
        {
            return Vector3.Cross(left, right);
        }

        /// <inheritdoc cref="Vector3.Dot"/>
        public static float Dot(this Vector3 left, Vector3 right)
        {
            return Vector3.Dot(left, right);
        }

        /// <summary>
        /// Is vector parallel to other vector?
        /// </summary>
        public static bool IsParallelTo(this Vector3 vector, Vector3 other, float tolerance = 1E-6f)
        {
            return System.Math.Abs(1.0 - System.Math.Abs(vector.Normalized().Dot(other.Normalized()))) <= tolerance;
        }

        /// <summary>
        /// Convert to matrix 4x4.
        /// </summary>
        public static Matrix ToMatrix(this in matrix3x4_t matrix)
        {
            return new Matrix
            {
                M11 = matrix.m00,
                M12 = matrix.m01,
                M13 = matrix.m02,

                M21 = matrix.m10,
                M22 = matrix.m11,
                M23 = matrix.m12,

                M31 = matrix.m20,
                M32 = matrix.m21,
                M33 = matrix.m22,

                M41 = matrix.m30,
                M42 = matrix.m31,
                M43 = matrix.m32,
                M44 = 1,
            };
        }
    }
}
