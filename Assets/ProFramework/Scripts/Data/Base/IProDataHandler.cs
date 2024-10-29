using System;

namespace ProFramework
{
    /// <summary>
    /// 数据处理接口，定义底层保存和加载方法。
    /// </summary>
    internal interface IProDataHandler
    {
        void OnSave<TData>(string key, TData value);

        void OnSave(string key, object value);

        TData OnLoad<TData>(string key);

        object OnLoad(string key, Type type);
    }
}