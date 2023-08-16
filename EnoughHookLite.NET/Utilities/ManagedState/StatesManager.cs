using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Utilities.ManagedState
{
    public sealed class StatesManager
    {
        private Dictionary<string, IStater> States;

        public StatesManager()
        {
            States = new Dictionary<string, IStater>();
        }

        public void AddStater(string id, IStater stater)
        {
            if (States.ContainsKey(id))
                return;
            States.Add(id, stater);
        }

        public void RemoveStater(string id)
        {
            if (!States.ContainsKey(id))
                return;
            States.Remove(id);
        }

        public void StartNew()
        {
            foreach (var stater in States.Values)
            {
                if (!stater.IsWorking)
                    stater.Start();
            }
        }
        public void RestartAll()
        {
            foreach (var stater in States.Values)
            {
                stater.Stop();
                stater.Start();
            }
        }
    }
}
