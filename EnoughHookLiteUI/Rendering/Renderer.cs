using EnoughHookLiteUI.Utils;
using EnoughHookLiteUI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.Rendering
{
    public sealed class Renderer
    {
        public Drawer Drawer { get; private set; }
        public SortedDictionary<ulong, ICanRender> Renderables { get; private set; }
        public int Count { get; private set; }

        public Renderer(EHLWindow wnd)
        {
            Renderables = new SortedDictionary<ulong, ICanRender>();

            Drawer = new Drawer(wnd, this);

            Drawer.Setup((ulong)wnd.Width, (ulong)wnd.Height);
            wnd.CustomDraw.Source = Drawer.WriteBitmap;

            Drawer.DrawCall = Render;
            Drawer.Start();
        }
        public void AddRender(ICanRender render)
        {
            Renderables.Add(Renderables.Last().Key + 1, render);
            Count++;
        }
        public void AddRender(ulong ind, ICanRender render)
        {
            Renderables.Add(ind, render);
            Count++;
        }
        public void RemoveRender(string name)
        {
            for (ulong i = 0; i < (ulong)Count; i++)
            {
                var rnd = Renderables[i];
                if (rnd.Name == name)
                {
                    Renderables.Remove(i);
                    Count--;
                    return;
                }
            }
        }
        public ICanRender GetRender(string name)
        {
            bool state = GetRender(name, out ICanRender render);
            if (!state)
                return null;
            return render;
        }
        public bool GetRender(string name, out ICanRender render)
        {
            ulong rco = (ulong)Renderables.Count;
            for (ulong i = 0; i < rco; i++)
            {
                var rnd = Renderables[i];
                if (rnd.Name == name)
                {
                    render = rnd;
                    return true;
                }
            }
            render = null;
            return false;
        }

        private void Render()
        {
            foreach (var rnd in Renderables)
            {
                rnd.Value.Render();
            }
        }
    }
}