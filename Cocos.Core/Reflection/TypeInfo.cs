using System.Collections.Concurrent;

namespace Cocos.Core.Reflection
{
    public class TypeInfo
    {
        public class Data
        {
            public Type Type { get; private set; }
            public TypeInfo? BaseTypeInfo { get; private set; }

            public Data(Type type)
            {
                Type = type;

                if (type.BaseType != default)
                {
                    BaseTypeInfo = new TypeInfo(type.BaseType);
                }
            }
        }

        private Data? data;
        private static ConcurrentDictionary<Type, Data> _typeData;

        static TypeInfo()
        {
            _typeData = new();
        }
        public TypeInfo(Type type)
        {
            if (!_typeData.TryGetValue(type, out data))
            {
                _typeData.TryAdd(type, data = new Data(type));
            }
        }
    }
}