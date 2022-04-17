using EnoughHookLite.Pointing.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing
{
    public sealed class TypeParser
    {
        private Type ClassType;

        private FieldInfo[] SignaturesFields;
        private FieldInfo[] NetvarsFields;
        private ulong NetvarFieldsLength;
        private ulong SignatureFieldsLength;

        private PointManager PointManager;

        public TypeParser(Type type, PointManager pm)
        {
            PointManager = pm;

            ClassType = type;

            var slist = new List<FieldInfo>();
            var nlist = new List<FieldInfo>();

            ParseFields(ClassType, nlist, slist);

            SignaturesFields = slist.ToArray();
            NetvarsFields = nlist.ToArray();

            NetvarFieldsLength = (ulong)NetvarsFields.LongLength;
            SignatureFieldsLength = (ulong)SignaturesFields.LongLength;
        }

        public void ParseFields(Type type, List<FieldInfo> nlist, List<FieldInfo> slist)
        {
            var bt = ClassType.BaseType;
            if (bt != typeof(object))
                ParseFields(bt, nlist, slist);

            var fields = type.GetFields(BindingFlags.Instance);
            var flen = fields.LongLength;

            for (long i = 0; i < flen; i++)
            {
                var field = fields[i];

                if (field.GetCustomAttribute<NetvarAttribute>() != null)
                {
                    nlist.Add(field);
                }
                else if (field.GetCustomAttribute<SignatureAttribute>() != null)
                {
                    slist.Add(field);
                }
            }
        }

        public bool ParseInstance(object instance)
        {
            for (ulong i = 0; i < SignatureFieldsLength; i++)
            {
                var field = SignaturesFields[i];

                var id = field.GetCustomAttribute<SignatureAttribute>().Id;
                if (!PointManager.AllocateSignature(id, out PointerCached pc))
                {
                    LogIt("failed get signature " + id);

                    return false;
                }

                field.SetValue(instance, pc);
            }

            for (ulong i = 0; i < NetvarFieldsLength; i++)
            {
                var field = NetvarsFields[i];

                var nmspace = field.GetCustomAttribute<NetvarAttribute>().NameSpace;
                if (!PointManager.AllocateNetvar(nmspace, out PointerCached pc))
                {
                    LogIt("failed get signature " + nmspace);

                    return false;
                }

                field.SetValue(instance, pc);
            }

            return true;
        }

        private void LogIt(string log)
        {
            App.Log.LogIt($"[TypeParser:{ClassType.Name}] " + log);
        }
    }
}
