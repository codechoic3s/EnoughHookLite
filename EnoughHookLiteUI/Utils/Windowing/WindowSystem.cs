using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.Utils.Windowing
{
    public sealed class WindowSystem
    {
        private Dictionary<Guid, Window> Windows;

        public WindowSystem()
        {
            Windows = new Dictionary<Guid, Window>();
        }

        public bool GetWindowByGUID(Guid id, out Window wnd)
        {
            return Windows.TryGetValue(id, out wnd);
        }
        public bool RemoveWindowByGUID(Guid id)
        {
            return Windows.Remove(id);
        }
        
        public Window AllocateWindow()
        {
            var wnd = new Window();
            Windows.Add(wnd.ID, wnd);
            return wnd;
        }
    }
}
