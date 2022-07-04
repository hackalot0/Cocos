using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Cocos.Core.Reflection
{
    public class TypeInfo
    {
        [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
        public class Member
        {
            public delegate object ValueGetter(object instance, object[] index);
            public delegate void ValueSetter(object instance, object value, object[] index);

            public string Name => MemberInfo.Name;
            public TypeInfo ReturnTypeInfo => returnTypeInfo;

            public TypeInfo Parent => parent;
            public MemberInfo MemberInfo { get; private set; }
            public MemberTypes MemberType => memberType;

            private MemberTypes memberType;
            private TypeInfo returnTypeInfo;
            private TypeInfo parent;
            private ValueGetter getter;
            private ValueSetter setter;

            public Member(MemberInfo memberInfo)
            {
                MemberInfo = memberInfo;
                Init();
            }

            protected virtual void Init()
            {
                memberType = MemberInfo.MemberType;
                parent = new TypeInfo(MemberInfo.DeclaringType);

                switch (MemberInfo)
                {
                    case PropertyInfo propertyInfo:
                        returnTypeInfo = new TypeInfo(propertyInfo.PropertyType);
                        getter = (instance, index) => propertyInfo.GetValue(instance, index);
                        setter = (instance, value, index) => propertyInfo.SetValue(instance, value, index);
                        break;

                    case FieldInfo fieldInfo:
                        returnTypeInfo = new TypeInfo(fieldInfo.FieldType);
                        getter = (instance, _) => fieldInfo.GetValue(instance);
                        setter = (instance, value, _) => fieldInfo.SetValue(instance, value);
                        break;

                    case EventInfo eventInfo:
                        returnTypeInfo = new TypeInfo(eventInfo.EventHandlerType);
                        getter = default; // Has to change to Add / Remove in future!
                        setter = default; // Has to change to Add / Remove in future!
                        break;

                    default:
                        break;
                }
            }

            public object GetValue(object instance = default, object[] index = default) => getter(instance, index);
            public void SetValue(object value, object instance = default, object[] index = default) => setter(instance, value, index);

            private string GetDebuggerDisplay()
            {
                var strList = new List<string>();
                var memberTypeName = Enum.GetName(typeof(MemberTypes), memberType);
                var returnTypeName = ReturnTypeInfo == default ? default : (ReturnTypeInfo.FullName ?? ReturnTypeInfo.Name);
                if (returnTypeName != default) strList.Add($"[{returnTypeName}]");
                if (memberTypeName != default) strList.Add($"{memberTypeName}: {Name}");

                return string.Join(" | ", strList);
            }
        }
        public class Data
        {
            public string Name => Type.Name;
            public string FullName => Type.FullName;

            public Type Type { get; private set; }
            public TypeInfo BaseTypeInfo { get; private set; }

            public IReadOnlyCollection<Member> MemberInfos => memberInfos.AsReadOnly();

            private List<Member> memberInfos;

            public Data(Type type)
            {
                Type = type;
            }

            public virtual void Populate()
            {
                memberInfos = new();
                if (Type != default)
                {
                    if (Type.BaseType != default)
                        BaseTypeInfo = new TypeInfo(Type.BaseType);

                    var members = Type.GetMembers();
                    if (members != default && members.Length > 0)
                        memberInfos.AddRange(members.Select(mi => new Member(mi)));
                }
            }
        }

        public string Name => data.Name;
        public string FullName => data.FullName;
        public TypeInfo BaseTypeInfo => data.BaseTypeInfo;
        public IReadOnlyCollection<Member> MemberInfos => data.MemberInfos;

        private readonly Data data;
        private static readonly ConcurrentDictionary<Type, Data> _typeData;

        static TypeInfo()
        {
            _typeData = new();
        }
        public TypeInfo(Data data)
        {
            this.data = data;
        }
        public TypeInfo(Type type) : this(GetTypeData(type)) { }

        public static TypeInfo GetTypeInfo(Type type) => new(GetTypeData(type));
        public static Data GetTypeData(Type type)
        {
            if (!_typeData.TryGetValue(type, out var data))
            {
                _typeData.TryAdd(type, data = new Data(type));
                data.Populate();
            }
            return data;
        }
    }
}