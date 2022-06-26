using System.Reflection;

namespace Cocos.Core.Reflection
{
    public class TypeMemberInfo
    {
        public delegate object? ValueGetter(object? instance, object?[]? index);
        public delegate void ValueSetter(object? instance, object? value, object?[]? index);

        public TypeInfo Parent { get; private set; }
        public MemberInfo MemberInfo { get; private set; }

        private MemberTypes memberType;
        private ValueGetter getter;
        private ValueSetter setter;

        public TypeMemberInfo(TypeInfo parent, MemberInfo memberInfo)
        {
            Parent = parent;
            MemberInfo = memberInfo;
            Init();
        }

        protected virtual void Init()
        {
            memberType = MemberInfo.MemberType;

            switch (MemberInfo)
            {
                case PropertyInfo propertyInfo:
                    getter = (instance, index) => propertyInfo.GetValue(instance, index);
                    setter = (instance, value, index) => propertyInfo.SetValue(instance, value, index);
                    break;

                case FieldInfo fieldInfo:
                    getter = (instance, _) => fieldInfo.GetValue(instance);
                    setter = (instance, value, _) => fieldInfo.SetValue(instance, value);
                    break;

                default:
                    break;
            }
        }

        public object? GetValue(object? instance = default, object?[]? index = default) => getter(instance, index);
        public void SetValue(object? value, object? instance = default, object?[]? index = default) => setter(instance, value, index);
    }
}