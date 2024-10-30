using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// PlayerPrefs 数据管理器
    /// </summary>
    public sealed class ProPlayerPrefsDataManager : ProAbstractDataManager<ProPlayerPrefsDataManager>
    {
        protected override string DataString => ProConst.PlayerPrefs;
        protected override EProDataType DataType => EProDataType.PlayerPrefs;

        private ProPlayerPrefsDataManager()
        {
        }
        
        protected override void OnSave<TData>(string key, TData value)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(key, jsonData);
            PlayerPrefs.Save();
        }

        protected override void OnSave(string key, object value)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(key, jsonData);
            PlayerPrefs.Save();
        }

        protected override TData OnLoad<TData>(string key)
        {
            Type type = typeof(TData);
            var jsonData = PlayerPrefs.GetString(key, string.Empty);
            if (!string.IsNullOrEmpty(jsonData))
            {
                try
                {
                    return JsonConvert.DeserializeObject<TData>(jsonData);
                }
                catch (JsonException ex)
                {
                    ProLog.LogError($"错误反序列化 key：'{key}' 类型：'{type}' 异常信息：{ex.Message}");
                }
            }

            return typeof(TData).IsValueType ? default : Activator.CreateInstance<TData>();
        }

        protected override object OnLoad(string key, Type type)
        {
            var jsonData = PlayerPrefs.GetString(key, string.Empty);
            if (!string.IsNullOrEmpty(jsonData))
            {
                try
                {
                    return JsonConvert.DeserializeObject(jsonData, type);
                }
                catch (JsonException ex)
                {
                    ProLog.LogError($"错误反序列化 key：'{key}' 类型：'{type}' 异常信息：{ex.Message}");
                }
            }

            return type.IsValueType ? default : Activator.CreateInstance(type);
        }

        protected override bool OnDelete(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                ProLog.LogWarning($"PlayerPrefs不存在键'{key}'，删除失败。");
                return false;
            }

            PlayerPrefs.DeleteKey(key);
            return true;
        }

        protected override void OnClear()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}