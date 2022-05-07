using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.Utils.Windowing
{
    public abstract class WindowDrawer
    {
        public abstract void Draw(Window wnd, WindowDrawStyle wds);
    }
}
