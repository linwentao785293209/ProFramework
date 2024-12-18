﻿using System;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 数据管理器基类
    /// </summary>
    /// <typeparam name="T">数据管理器类型</typeparam>
    public abstract class ProAbstractDataManager<T>
        : ProCSharpSingleton<T>, IProDataManager, IStreamingAssetsPath, IPersistentDataPath
        where T : ProAbstractDataManager<T>
    {
        public string StreamingAssetsPath => $"{Application.streamingAssetsPath}/{ProConst.Data}/{DataString}/";
        public string PersistentDataPath => $"{Application.persistentDataPath}/{ProConst.Data}/{DataString}/";

        protected abstract string DataString { get; }
        protected abstract EProDataType DataType { get; }
        protected abstract string DataExtension { get; }

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

        public bool Delete(string key)
        {
            ProDataValidator.ValidateKey(key);
            return OnDelete(key);
        }

        public void Clear()
        {
            OnClear();
        }

        protected abstract void OnSave<TData>(string key, TData value);

        protected abstract void OnSave(string key, object value);

        protected abstract TData OnLoad<TData>(string key);

        protected abstract object OnLoad(string key, Type type);

        protected abstract bool OnDelete(string key);

        protected abstract void OnClear();
    }
}