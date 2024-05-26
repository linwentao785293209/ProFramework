using System;

namespace ProFramework
{
    public abstract class ProAbstractDataManage<T> : ProSingletonInSystem<T>, IProDataManager where T : class
    {
        public abstract void Save<T1>(string key, T1 value);

        public abstract void Save(string key, object value);

        public abstract T1 Load<T1>(string key) where T1 : class, new();

        public abstract object Load(string key, Type type);
    }
}