using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cocos.Core.Reflection
{
    public class TypeInfo
    {
        public class Data
        {
            public Type Type { get; private set; }
            public TypeInfo BaseTypeInfo { get; private set; }

            public List<TypeMemberInfo> MemberInfos { get; private set; }

            public Data(Type type)
            {
                Type = type;
            }

            public virtual void Init()
            {
                MemberInfos = new();
                if (Type != default)
                {
                    if (Type.BaseType != default)
                    {
                        BaseTypeInfo = new TypeInfo(Type.BaseType);
                    }

                    var members = Type.GetMembers();
                    if (members != default && members.Length > 0)
                    {
                        MemberInfos.AddRange(members.Select(mi => new TypeMemberInfo(mi)));
                    }
                }
            }
        }

        private Data data;
        private static ConcurrentDictionary<Type, Data> _typeData;

        static TypeInfo()
        {
            _typeData = new();
        }
        public TypeInfo(Data data)
        {
            this.data = data;
        }
        public TypeInfo(Type type) : this(GetTypeData(type)) { }

        public static TypeInfo GetTypeInfo(Type type) => new TypeInfo(GetTypeData(type));
        public static Data GetTypeData(Type type)
        {
            if (!_typeData.TryGetValue(type, out var data)) _typeData.TryAdd(type, data = new Data(type));
            return data;
        }
    }
}