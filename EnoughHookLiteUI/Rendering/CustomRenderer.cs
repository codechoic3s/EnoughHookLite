using EnoughHookLite;
using EnoughHookLite.Logging;
using EnoughHookLite.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.Rendering
{
    public sealed class CustomRenderer : ICanRender
    {
        public List<(string, Script)> DrawList;

        private LogEntry LogCustomRenderer;
        public CustomRenderer()
        {
            LogCustomRenderer = new LogEntry(() => { return "[CustomRenderer] "; });
            App.LogHandler.AddEntry("CustomRenderer", LogCustomRenderer);
            DrawList = new List<(string, Script)>();
        }

        public string Name => nameof(CustomRenderer);

        public void Render()
        {
            int dlco = DrawList.Count;
            for (int i = 0; i < dlco; i++)
            {
                var del = DrawList[i];
                var name = del.Item1;

                try
                {
                    del.Item2.JSEngine.Invoke(name);
                }
                catch (Exception ex)
                {
                    DrawList.RemoveAt(i);
                    LogCustomRenderer.Log($"Failed render '{name}' in script '{del.Item2.Name}', callback is removed.");
                    del.Item2.HandleException(ex);
                }
            }
        }
    }
}
