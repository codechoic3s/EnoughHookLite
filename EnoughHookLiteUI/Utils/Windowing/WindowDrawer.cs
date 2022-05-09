﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.Utils.Windowing
{
    public sealed class WindowDrawer
    {
        public Action<Window, WindowDrawStyle> Draw;
    }
}