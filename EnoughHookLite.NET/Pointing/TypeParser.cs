using EnoughHookLite.Logging;
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

        private LogEntry LogTypeParser;

        public TypeParser(Type type, PointManager pm, bool maintype = false)
        {
            PointManager = pm;

            ClassType = type;

            var slist = new List<(FieldInfo, SignatureAttribute)>();
            var nlist = new List<(FieldInfo, NetvarAttribute)>();

            ParseFields(ClassType, maintype, ref nlist, ref slist);

            SignaturesFields = slist.ToArray();
            NetvarsFields = nlist.ToArray();

            NetvarFieldsLength = (ulong)NetvarsFields.LongLength;
            SignatureFieldsLength = (ulong)SignaturesFields.LongLength;

            LogTypeParser = new LogEntry(() => { return $"[TypeParser:{ClassType.Name}] "; });
            App.LogHandler.AddEntry($"TypeParser:{ClassType.Name}", LogTypeParser);
            //LogIt($"netvars: {NetvarFieldsLength} signatures: {SignatureFieldsLength}");
        }

        private void ParseFields(Type type, bool maintype, ref List<(FieldInfo, NetvarAttribute)> nlist, ref List<(FieldInfo, SignatureAttribute)> slist)
        {
            var bt = ClassType.BaseType;
            if (bt != typeof(object) && !maintype)
                ParseFields(bt, maintype, ref nlist, ref slist);

            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
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

                var id = field.Item2.Id;
                if (!PointManager.AllocateSignature(id, out PointerCached pc))
                {
                    LogTypeParser.Log("failed get signature " + id);

                    return false;
                }
                
                field.Item1.SetValue(instance, pc);
            }

            for (ulong i = 0; i < NetvarFieldsLength; i++)
            {
                var field = NetvarsFields[i];

                var nmspace = field.Item2.NameSpace;
                if (!PointManager.AllocateNetvar(nmspace, out PointerCached pc))
                {
                    LogTypeParser.Log("failed get signature " + nmspace);

                    return false;
                }

                field.Item1.SetValue(instance, pc);
            }

            return true;
        }
    }
}
