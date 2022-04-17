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

        private (FieldInfo, SignatureAttribute)[] SignaturesFields;
        private (FieldInfo, NetvarAttribute)[] NetvarsFields;
        private ulong NetvarFieldsLength;
        private ulong SignatureFieldsLength;

        private PointManager PointManager;

        public TypeParser(Type type, PointManager pm)
        {
            PointManager = pm;

            ClassType = type;

            var slist = new List<(FieldInfo, SignatureAttribute)>();
            var nlist = new List<(FieldInfo, NetvarAttribute)>();

            ParseFields(ClassType, nlist, slist);

            SignaturesFields = slist.ToArray();
            NetvarsFields = nlist.ToArray();

            NetvarFieldsLength = (ulong)NetvarsFields.LongLength;
            SignatureFieldsLength = (ulong)SignaturesFields.LongLength;
        }

        public void ParseFields(Type type, List<(FieldInfo, NetvarAttribute)> nlist, List<(FieldInfo, SignatureAttribute)> slist)
        {
            var bt = ClassType.BaseType;
            if (bt != null)
                ParseFields(bt, nlist, slist);

            var fields = type.GetFields(BindingFlags.Instance);
            var flen = fields.LongLength;

            for (long i = 0; i < flen; i++)
            {
                var field = fields[i];

                if (field.GetCustomAttribute<NetvarAttribute>() is NetvarAttribute na)
                {
                    nlist.Add((field, na));
                }
                else if (field.GetCustomAttribute<SignatureAttribute>() is SignatureAttribute sa)
                {
                    slist.Add((field, sa));
                }
            }
        }

        public bool ParseInstance(object instance)
        {
            for (ulong i = 0; i < SignatureFieldsLength; i++)
            {
                var field = SignaturesFields[i];

                if (!PointManager.AllocateSignature(field.Item2.Id, out PointerCached pc))
                {
                    LogIt("failed get signature " + field.Item2.Id);

                    return false;
                }

                field.Item1.SetValue(instance, pc);
            }

            for (ulong i = 0; i < NetvarFieldsLength; i++)
            {
                var field = NetvarsFields[i];

                if (!PointManager.AllocateNetvar(field.Item2.NameSpace, out PointerCached pc))
                {
                    LogIt("failed get signature " + field.Item2.NameSpace);

                    return false;
                }

                field.Item1.SetValue(instance, pc);
            }

            return true;
        }

        private void LogIt(string log)
        {
            App.Log.LogIt($"[TypeParser:{ClassType.Name}] " + log);
        }
    }
}
