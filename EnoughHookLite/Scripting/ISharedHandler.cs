using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Scripting
{
    public interface ISharedHandler
    {
        bool AddDelegateSafe(string name, Delegate del);
        bool AddTypeSafe(string name, Type type);
        bool AddValueSafe(string name, object obj);
        bool AddEventSafe(string name, List<(string, Script)> callbacklist);

        void AddDelegate(string name, Delegate del);
        void AddType(string name, Type type);
        void AddValue(string name, object obj);
        void AddEvent(string name, List<(string, Script)> callbacklist);
        void RemoveEvent(ScriptEvent @event);
    }
}
