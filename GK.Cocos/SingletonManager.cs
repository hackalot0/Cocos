using System;
using System.Collections.Generic;
using System.Linq;

namespace GK.Cocos
{
    public class SingletonManager : CocosManager
    {
        public CocosRuntimeState CocosRuntimeState { get; set; }
        public IDictionary<Type, object> Instances => instances;

        private IDictionary<Type, object> instances;

        public bool Add<T>(T instance)
        {
            bool add = !Contains(instance);
            if (add) instances.Add(typeof(T), instance);
            return add;
        }
        public bool Get<T>(out T instance)
        {
            bool get = Contains(typeof(T), out Type t);
            instance = get ? (T)instances[t] : default(T);
            return get;
        }
        public bool Contains<T>(T instance) => Contains(typeof(T));
        public bool Contains(Type instanceType) => instances.Keys.FirstOrDefault(instanceType.IsAssignableFrom) != null;
        public bool Contains(Type instanceType, out Type foundType) => (foundType = instances.Keys.FirstOrDefault(instanceType.IsAssignableFrom)) != null;

        protected override void Init()
        {
            instances = new Dictionary<Type, object>();
            Add(this);
        }
    }
}