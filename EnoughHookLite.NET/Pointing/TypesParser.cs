using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Pointing
{
    public sealed class TypesParser
    {
        private Dictionary<Type, TypeParser> typeParsers;

        public PointManager PointManager { get; private set; }
        public TypesParser(PointManager pm)
        {
            typeParsers = new Dictionary<Type, TypeParser>();
            PointManager = pm;
        }

        public bool TryParse(object obj, bool maintype = false)
        {
            var otype = obj.GetType();
            if (!typeParsers.TryGetValue(otype, out TypeParser typeParser))
            {
                typeParser = new TypeParser(otype, PointManager, maintype);

                if (!typeParser.ParseInstance(obj))
                    return false;

                typeParsers.Add(otype, typeParser);
                return true;
            }

            return typeParser.ParseInstance(obj);
        }
    }
}
