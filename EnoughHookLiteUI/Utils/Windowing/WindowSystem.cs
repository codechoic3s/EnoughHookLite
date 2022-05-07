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
        private Dictionary<Guid, Window> VisibleWindows;

        public WindowSystem()
        {
            Windows = new Dictionary<Guid, Window>();
        }

        public bool ShowWindow(Guid guid)
        {
            if (!Windows.ContainsKey(guid))
                return false;
            if (VisibleWindows.ContainsKey(guid))
                return true;

            VisibleWindows.Add(guid, Windows[guid]);
            return true;
        }
        public bool HideWindow(Guid guid)
        {
            if (!Windows.ContainsKey(guid))
                return false;
            if (!VisibleWindows.ContainsKey(guid))
                return true;

            VisibleWindows.Remove(guid);
            return true;
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
            var wnd = new Window(this);
            Windows.Add(wnd.ID, wnd);
            return wnd;
        }
    }
}
