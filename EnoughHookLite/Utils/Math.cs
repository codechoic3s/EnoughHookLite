using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
