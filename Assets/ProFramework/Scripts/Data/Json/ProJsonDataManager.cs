using System;
using System.IO;
using UnityEngine;
using LitJson;
using Newtonsoft.Json;


namespace ProFramework
{
    public class ProJsonDataManager : ProAbstractDataManage<ProJsonDataManager>
    {
        private ProJsonDataManager()
        {
        }

        private EProJsonType _jsonType = EProJsonType.NewtonsoftJson;


        private string streamingAssetsPath => $"{Application.streamingAssetsPath}/{ProConst.JsonDataPath}";

        private string persistentDataPath => $"{Application.persistentDataPath}/{ProConst.JsonDataPath}";

        public void SetJsonType(EProJsonType jsonType)
        {
            _jsonType = jsonType;
        }

        public override void Save<T>(string key, T value)
        {
            Save(key, (object)value);
        }

        public override void Save(string key, object value)
        {
            string path = persistentDataPath + key + ".json";

            // 检查路径是否存在，如果不存在则创建路径
            if (!Directory.Exists(Path.GetDirectoryName(persistentDataPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(persistentDataPath));
            }

            string jsonStr = "";
            switch (_jsonType)
            {
                case EProJsonType.JsonUtility:
                    jsonStr = JsonUtility.ToJson(value);
                    break;
                case EProJsonType.LitJson:
                    jsonStr = JsonMapper.ToJson(value);
                    break;
                case EProJsonType.NewtonsoftJson:
                    jsonStr = JsonConvert.SerializeObject(value);
                    break;
            }

            File.WriteAllText(path, jsonStr);
        }

        public override T Load<T>(string key)
        {
            string path = persistentDataPath + key + ".json";

            if (!File.Exists(path))
            {
                path = streamingAssetsPath + key + ".json";

                if (!File.Exists(path))
                {
                    ProLog.LogWarning($"文件{key}未找到,返回默认实例！");
                    return new T();
                }
            }

            string jsonStr = File.ReadAllText(path);
            T value = default(T);

            switch (_jsonType)
            {
                case EProJsonType.JsonUtility:
                    value = JsonUtility.FromJson<T>(jsonStr);
                    break;
                case EProJsonType.LitJson:
                    value = JsonMapper.ToObject<T>(jsonStr);
                    break;
                case EProJsonType.NewtonsoftJson:
                    value = JsonConvert.DeserializeObject<T>(jsonStr);
                    break;
            }

            return value;
        }

        public override object Load(string key, Type type)
        {
            string path = persistentDataPath + key + ".json";

            // 首先尝试从持久化数据路径加载
            if (!File.Exists(path))
            {
                path = streamingAssetsPath + key + ".json";

                // 如果在StreamingAssets路径下也不存在，则返回null或其他默认值处理
                if (!File.Exists(path))
                {
                    ProLog.LogWarning($"文件{key}未找到,返回默认实例！");
                    // 使用反射获取类型的默认值
                    return Activator.CreateInstance(type);
                }
            }

            string jsonStr = File.ReadAllText(path);

            object value = null;
            switch (_jsonType)
            {
                case EProJsonType.JsonUtility:
                    value = JsonUtility.FromJson(jsonStr, type);
                    break;
                case EProJsonType.LitJson:
                    value = JsonMapper.ToObject(jsonStr, type);
                    break;
                case EProJsonType.NewtonsoftJson:
                    value = JsonConvert.DeserializeObject(jsonStr, type);
                    break;
            }

            return value;
        }
    }
}