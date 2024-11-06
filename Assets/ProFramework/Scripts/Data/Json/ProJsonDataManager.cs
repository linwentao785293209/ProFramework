using System;
using UnityEngine;
using LitJson;
using Newtonsoft.Json;

namespace ProFramework
{
    /// <summary>
    /// JSON 数据管理器
    /// </summary>
    public class ProJsonDataManager : ProAbstractDataManager<ProJsonDataManager>
    {
        private EProJsonType _jsonType = EProJsonType.NewtonsoftJson;

        protected override string DataString => ProConst.Json;
        protected override EProDataType DataType => EProDataType.Json;
        protected override string DataExtension => "json";

        private ProJsonDataManager()
        {
        }

        public void SetJsonType(EProJsonType jsonType)
        {
            _jsonType = jsonType;
        }

        protected override void OnSave<TData>(string key, TData value)
        {
            OnSave(key, (object)value);
        }

        protected override void OnSave(string key, object value)
        {
            string path = ProPathUtil.GetFilePath(PersistentDataPath, key, DataExtension);
            ProDirectoryUtil.CreateDirectory(ProPathUtil.GetDirectoryName(path));

            string jsonStr = Serialize(value);
            try
            {
                ProFileUtil.WriteAllText(path, jsonStr);
            }
            catch (Exception e)
            {
                ProLog.LogError($"保存 JSON 数据失败，错误信息：{e.Message}\n{e.StackTrace}");
            }
        }

        protected override TData OnLoad<TData>(string key)
        {
            return (TData)OnLoad(key, typeof(TData));
        }

        protected override object OnLoad(string key, Type type)
        {
            string path = ProPathUtil.GetFilePath(PersistentDataPath, key, DataExtension);
            if (!ProFileUtil.FileExists(path))
            {
                path = ProPathUtil.GetFilePath(StreamingAssetsPath, key, DataExtension);
                if (!ProFileUtil.FileExists(path))
                {
                    ProLog.LogWarning($"文件 {key} 未找到, 返回默认实例！");
                    return Activator.CreateInstance(type);
                }
            }

            string jsonStr = ProFileUtil.ReadAllText(path);
            return Deserialize(jsonStr, type);
        }

        protected override bool OnDelete(string key)
        {
            string path = ProPathUtil.GetFilePath(PersistentDataPath, key, DataExtension);
            if (ProFileUtil.FileExists(path))
            {
                try
                {
                    ProFileUtil.DeleteFile(path);
                    return true;
                }
                catch (Exception e)
                {
                    ProLog.LogError($"删除 JSON 文件失败，错误信息：{e.Message}\n{e.StackTrace}");
                    return false;
                }
            }
            else
            {
                ProLog.LogWarning($"文件 {key} 不存在，无法删除。");
                return false;
            }
        }

        protected override void OnClear()
        {
            string[] files = ProFileUtil.GetFiles(PersistentDataPath, DataExtension);
            foreach (string file in files)
            {
                try
                {
                    ProFileUtil.DeleteFile(file);
                }
                catch (Exception e)
                {
                    ProLog.LogError($"清理 JSON 文件失败，错误信息：{e.Message}\n{e.StackTrace}");
                }
            }

            ProLog.LogInfo($"已清理 {files.Length} 个 JSON 文件。");
        }

        private string Serialize(object value)
        {
            return _jsonType switch
            {
                EProJsonType.JsonUtility => JsonUtility.ToJson(value),
                EProJsonType.LitJson => JsonMapper.ToJson(value),
                EProJsonType.NewtonsoftJson => JsonConvert.SerializeObject(value),
                _ => throw new NotSupportedException($"不支持的 JSON 类型: {_jsonType}")
            };
        }

        private object Deserialize(string jsonStr, Type type)
        {
            return _jsonType switch
            {
                EProJsonType.JsonUtility => JsonUtility.FromJson(jsonStr, type),
                EProJsonType.LitJson => JsonMapper.ToObject(jsonStr, type),
                EProJsonType.NewtonsoftJson => JsonConvert.DeserializeObject(jsonStr, type),
                _ => throw new NotSupportedException($"不支持的 JSON 类型: {_jsonType}")
            };
        }
    }
}