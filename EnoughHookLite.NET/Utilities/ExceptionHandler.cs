using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities
{
    public static class ExceptionHandler
    {
        public static string HandleEception(Exception ex, bool full = false)
        {
            if (full)
            {
                return
                    "\n+EXCEPTION+{\n" + ex.ToString() + "}\n" +
                    "+STACKTRACE+{\n" + ex.StackTrace + "}";
            }
            else
                return ex.Message;
        }
    }
}
