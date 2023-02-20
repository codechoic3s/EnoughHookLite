using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting.Integration
{
    public interface ISharedGlobalHandler
    {
        bool AddTypeSafe(string name, Type type);

        void AddType(string name, Type type);

        bool AddEventSafe(string name, List<(string, Script)> callbacklist);
        void AddEvent(string name, List<(string, Script)> callbacklist);
        void RemoveEvent(ScriptEvent @event);
    }
}
