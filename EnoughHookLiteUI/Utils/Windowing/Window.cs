using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.Utils.Windowing
{
    public sealed class Window
    {
        public Guid ID { get; private set; }

        public string Title { get; private set; }

        private WindowSystem WindowSystem;
        public Window(WindowSystem wndsys)
        {
            ID = Guid.NewGuid();
            WindowSystem = wndsys;
        }

        public void Show()
        {
            WindowSystem.ShowWindow(ID);
        }

        public void Hide()
        {
            WindowSystem.HideWindow(ID);
        }
    }
}
