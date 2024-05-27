using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ProFramework;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 2进制数据管理器
    /// </summary>
    public class ProBinaryDataManager : ProAbstractDataManage<ProBinaryDataManager>
    {
        private string streamingAssetsPath => $"{Application.streamingAssetsPath}/{ProConst.BinaryDataPath}";

        private string persistentDataPath => $"{Application.persistentDataPath}/{ProConst.BinaryDataPath}";


        private ProBinaryDataManager()
        {
        }


        public override void Save<T>(string key, T value)
        {
            Save(key, (object)value);
        }


        /// <summary>
        /// 存储类对象数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public override void Save(string key, object value)
        {
            //先判断路径文件夹有没有
            if (!Directory.Exists(persistentDataPath))
                Directory.CreateDirectory(persistentDataPath);

            using (FileStream fileStream =
                   new FileStream(persistentDataPath + key + ".tao", FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, value);
                fileStream.Close();
            }
        }


        /// <summary>
        /// 读取2进制数据转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public override T Load<T>(string key) where T : class
        {
            string path = persistentDataPath + key + ".tao";

            if (!File.Exists(path))
            {
                path = streamingAssetsPath + key + ".tao";

                if (!File.Exists(path))
                {
                    ProLog.LogWarning($"文件{key}未找到,返回默认实例！");
                    return new T();
                }
            }

            T value;
            using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                value = binaryFormatter.Deserialize(fileStream) as T;
                fileStream.Close();
            }

            return value;
        }

        /// <summary>
        /// 读取2进制数据转换成对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public override object Load(string key, Type type)
        {
            string path = persistentDataPath + key + ".tao";

            if (!File.Exists(path))
            {
                path = streamingAssetsPath + key + ".tao";

                if (!File.Exists(path))
                {
                    ProLog.LogWarning($"文件{key}未找到,返回默认实例！");
                    return Activator.CreateInstance(type);
                }
            }


            object value;
            using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                value = binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }

            // 确保反序列化的对象是请求的类型
            if (value != null && type.IsInstanceOfType(value))
            {
                return value;
            }

            // 如果类型不匹配，返回null
            return Activator.CreateInstance(type);
        }
    }
}