using System;

namespace ProFramework
{
    public interface IProDataManager
    {
        void Save<T>(string key, T value);

        void Save(string key, object value);

        T Load<T>(string key) where T : new();

        object Load(string key, Type type);
    }
}