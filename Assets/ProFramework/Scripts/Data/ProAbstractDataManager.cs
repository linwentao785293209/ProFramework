using System;

namespace ProFramework
{
    /// <summary>
    /// 数据管理器基类
    /// </summary>
    /// <typeparam name="T">数据管理器类型</typeparam>
    public abstract class ProAbstractDataManager<T> : ProCSharpSingleton<T>, IProDataManager where T : ProCSharpSingleton<T>
    {
        public abstract void Save<TData>(string key, TData value);

        public abstract void Save(string key, object value);

        public abstract TData Load<TData>(string key) where TData : class, new();

        public abstract object Load(string key, Type type);
    }
}