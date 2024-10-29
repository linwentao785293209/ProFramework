using System;

namespace ProFramework
{
    /// <summary>
    /// 数据管理器基类
    /// </summary>
    /// <typeparam name="T">数据管理器类型</typeparam>
    public abstract class ProAbstractDataManager<T> : ProCSharpSingleton<T>, IProDataManager
        where T : ProAbstractDataManager<T>
    {
        public void Save<TData>(string key, TData value)
        {
            ProDataValidator.ValidateKey(key);
            ProDataValidator.ValidateValue(value);
            OnSave(key, value);
        }

        public void Save(string key, object value)
        {
            ProDataValidator.ValidateKey(key);
            ProDataValidator.ValidateValue(value);
            OnSave(key, value);
        }

        public TData Load<TData>(string key)
        {
            ProDataValidator.ValidateKey(key);
            return OnLoad<TData>(key);
        }

        public object Load(string key, Type type)
        {
            ProDataValidator.ValidateKey(key);
            ProDataValidator.ValidateType(type);
            return OnLoad(key, type);
        }

        protected abstract void OnSave<TData>(string key, TData value);

        protected abstract void OnSave(string key, object value);

        protected abstract TData OnLoad<TData>(string key);

        protected abstract object OnLoad(string key, Type type);
    }
}