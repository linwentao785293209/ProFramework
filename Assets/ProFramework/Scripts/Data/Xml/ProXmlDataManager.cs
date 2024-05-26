using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace ProFramework
{
    public class ProXmlDataManager : ProAbstractDataManage<ProXmlDataManager>
    {
        private ProXmlDataManager()
        {
            
        }
        
        private string streamingAssetsPath => $"{Application.streamingAssetsPath}/{ProConst.XmlDataPath}";

        private string persistentDataPath => $"{Application.persistentDataPath}/{ProConst.XmlDataPath}";

        public override void Save<T>(string key, T value)
        {
            Save(key, (object)value);
        }

        public override void Save(string key, object value)
        {
            string path = persistentDataPath + key + ".xml";
            
            // 检查路径是否存在，如果不存在则创建路径
            if (!Directory.Exists(Path.GetDirectoryName(persistentDataPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(persistentDataPath));
            }

            
            
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());
                xmlSerializer.Serialize(streamWriter, value);
            }
        }

        public override T Load<T>(string key)
        {
            return (T)Load(key, typeof(T));
        }

        public override object Load(string key, Type type)
        {
            string path = persistentDataPath + key + ".xml";
            if (!File.Exists(path))
            {
                path = streamingAssetsPath + key + ".xml";

                if (!File.Exists(path))
                {
                    ProLog.LogWarning($"文件{key}未找到,返回默认实例！");
                    return Activator.CreateInstance(type);
                }
            }

            using (StreamReader streamReader = new StreamReader(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                return xmlSerializer.Deserialize(streamReader);
            }
        }
    }
}