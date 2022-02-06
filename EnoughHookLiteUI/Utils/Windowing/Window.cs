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

        public Window()
        {
            ID = Guid.NewGuid();
        }

        public void Show()
        {

        }

        public void Hide()
        {

        }
    }
}
