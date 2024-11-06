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
        protected override string DataExtension => "";

        private ProPlayerPrefsDataManager()
        {
        }

        protected override void OnSave<TData>(string key, TData value)
        {
            SaveToPlayerPrefs(key, value);
        }

        protected override void OnSave(string key, object value)
        {
            SaveToPlayerPrefs(key, value);
        }

        private void SaveToPlayerPrefs<TData>(string key, TData value)
        {
            Type type = value.GetType();
            try
            {
                var jsonData = JsonConvert.SerializeObject(value);
                PlayerPrefs.SetString(key, jsonData);
                PlayerPrefs.Save();
            }
            catch (Exception ex)
            {
                ProLog.LogError($"存储时错误序列化 key：'{key}' 类型：'{type}' 异常信息：{ex.Message}");
            }
        }

        protected override TData OnLoad<TData>(string key)
        {
            return (TData)LoadFromPlayerPrefs(key, typeof(TData));
        }

        protected override object OnLoad(string key, Type type)
        {
            return LoadFromPlayerPrefs(key, type);
        }

        private object LoadFromPlayerPrefs(string key, Type type)
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
                    ProLog.LogError($"读取时错误反序列化 key：'{key}' 类型：'{type}' 异常信息：{ex.Message}");
                }
            }

            ProLog.LogWarning($"读取时错误反序列化，返回默认类型");
            return type.IsValueType ? Activator.CreateInstance(type) : null;
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