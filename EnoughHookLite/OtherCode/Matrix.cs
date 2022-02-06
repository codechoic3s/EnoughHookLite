using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utils
{
    public struct Matrix
    {
        public float M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44;

        public override string ToString()
        {
            return $"{M11} {M12} {M13} {M14} {M21} {M22} {M23} {M24} {M31} {M32} {M33} {M34} {M41} {M42} {M43} {M44}";
        }
    }
}
