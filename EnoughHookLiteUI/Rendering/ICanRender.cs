using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLiteUI.Rendering
{
    public interface ICanRender
    {
        void Render();
        string Name { get; }
    }
}
