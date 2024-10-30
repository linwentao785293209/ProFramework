using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// Xml 数据管理器
    /// </summary>
    public class ProXmlDataManager : ProAbstractDataManager<ProXmlDataManager>
    {
        protected override string DataString => ProConst.Xml;
        protected override EProDataType DataType => EProDataType.Xml;

        private readonly ConcurrentDictionary<Type, XmlSerializer> _xmlSerializerCache =
            new ConcurrentDictionary<Type, XmlSerializer>();

        private ProXmlDataManager()
        {
        }

        protected override void OnSave<TData>(string key, TData value)
        {
            OnSave(key, (object)value);
        }

        protected override void OnSave(string key, object value)
        {
            string path = ProPathUtil.GetFilePath(PersistentDataPath, key, "xml");
            ProDirectoryUtil.CreateDirectory(Path.GetDirectoryName(path));

            try
            {
                using (var fileStream = ProFileStreamUtil.OpenFile(path, FileMode.Create, FileAccess.Write))
                {
                    var xmlSerializer = GetSerializer(value.GetType());
                    xmlSerializer.Serialize(fileStream, value);
                }
            }
            catch (Exception e)
            {
                ProLog.LogError($"保存 XML 数据失败，错误信息：{e.Message}\n{e.StackTrace}");
            }
        }

        protected override TData OnLoad<TData>(string key)
        {
            return (TData)OnLoad(key, typeof(TData));
        }

        protected override object OnLoad(string key, Type type)
        {
            string path = ProPathUtil.GetFilePath(PersistentDataPath, key, "xml");
            if (!ProFileUtil.FileExists(path))
            {
                path = ProPathUtil.GetFilePath(StreamingAssetsPath, key, "xml");

                if (!ProFileUtil.FileExists(path))
                {
                    ProLog.LogWarning($"文件 {key} 未找到, 返回默认实例！");
                    return Activator.CreateInstance(type);
                }
            }

            using (var fileStream = ProFileStreamUtil.OpenFile(path, FileMode.Open, FileAccess.Read))
            {
                var xmlSerializer = GetSerializer(type);
                return xmlSerializer.Deserialize(fileStream);
            }
        }

        protected override bool OnDelete(string key)
        {
            string path = ProPathUtil.GetFilePath(PersistentDataPath, key, "xml");
            if (ProFileUtil.FileExists(path))
            {
                try
                {
                    ProFileUtil.DeleteFile(path);
                    return true;
                }
                catch (Exception e)
                {
                    ProLog.LogError($"删除 XML 文件失败，错误信息：{e.Message}\n{e.StackTrace}");
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
            string[] files = ProFileUtil.GetFiles(PersistentDataPath, "*.xml");
            foreach (string file in files)
            {
                try
                {
                    ProFileUtil.DeleteFile(file);
                }
                catch (Exception e)
                {
                    ProLog.LogError($"清理 XML 文件失败，错误信息：{e.Message}\n{e.StackTrace}");
                }
            }

            ProLog.LogInfo($"已清理 {files.Length} 个 XML 文件。");
        }

        private XmlSerializer GetSerializer(Type type)
        {
            return _xmlSerializerCache.GetOrAdd(type, t => new XmlSerializer(t));
        }
    }
}