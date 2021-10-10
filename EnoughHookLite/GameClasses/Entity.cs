using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.GameClasses
{
    public class Entity
    {
        public App App;
        public int Pointer;

        public Entity(App app)
        {
            App = app;
        }
    }
}
