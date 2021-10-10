using EnoughHookLite.GameClasses;
using EnoughHookLite.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Modules
{
    public class Client
    {
        public Module ClientModule;
        public App App;

        public Client(Module m, App app)
        {
            ClientModule = m;
            App = app;
            EntityList = new EntityList(App);
        }

        public void Start()
        {
            EntityList.Start();
        }

        public EntityList EntityList;
        
    }
}
