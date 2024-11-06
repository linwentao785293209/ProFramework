using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 二进制数据管理器
    /// </summary>
    public class ProBinaryDataManager : ProAbstractDataManager<ProBinaryDataManager>
    {
        protected override string DataString => ProConst.Binary;
        protected override EProDataType DataType => EProDataType.Binary;
        protected override string DataExtension => "tao";

        private ProBinaryDataManager()
        {
        }

        protected override void OnSave<TData>(string key, TData value)
        {
            OnSave(key, (object)value);
        }

        protected override void OnSave(string key, object value)
        {
            string path = ProPathUtil.GetFilePath(PersistentDataPath, key, DataExtension);
            ProDirectoryUtil.CreateDirectory(Path.GetDirectoryName(path));

            try
            {
                using (FileStream fileStream = ProFileStreamUtil.OpenFile(path, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, value);
                }
            }
            catch (Exception e)
            {
                ProLog.LogError($"保存二进制数据失败，错误信息：{e.Message}\n{e.StackTrace}");
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
                    return Activator.CreateInstance(type); // 或者返回 null
                }
            }

            object value;
            try
            {
                using (FileStream fileStream = ProFileStreamUtil.OpenFile(path, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    value = binaryFormatter.Deserialize(fileStream);
                }
            }
            catch (Exception e)
            {
                ProLog.LogError($"加载二进制数据失败，错误信息：{e.Message}\n{e.StackTrace}");
                return Activator.CreateInstance(type); // 或者返回 null
            }

            // 确保反序列化的对象是请求的类型
            if (value != null && type.IsInstanceOfType(value))
            {
                return value;
            }

            // 如果类型不匹配，返回 null
            return Activator.CreateInstance(type);
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
                    ProLog.LogError($"删除二进制文件失败，错误信息：{e.Message}\n{e.StackTrace}");
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
                    ProLog.LogError($"清理二进制文件失败，错误信息：{e.Message}\n{e.StackTrace}");
                }
            }

            ProLog.LogInfo($"已清理 {files.Length} 个二进制文件。");
        }
    }
}