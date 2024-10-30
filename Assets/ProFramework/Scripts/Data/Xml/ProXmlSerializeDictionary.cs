using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 一个可序列化的字典类，支持 XML 序列化和反序列化。
    /// </summary>
    /// <typeparam name="TKey">字典的键类型。</typeparam>
    /// <typeparam name="TValue">字典的值类型。</typeparam>
    public class ProXmlSerializeDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        /// <summary>
        /// 获取 XML 架构信息。此实现始终返回 null。
        /// </summary>
        /// <returns>始终返回 null。</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 从 XML 读取数据并填充字典。
        /// </summary>
        /// <param name="xmlReader">XML 读取器，用于读取 XML 数据。</param>
        public void ReadXml(XmlReader xmlReader)
        {
            // 创建用于序列化键和值的 XmlSerializer 实例
            XmlSerializer keyXmlSerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueXmlSerializer = new XmlSerializer(typeof(TValue));

            // 读取 XML 节点
            xmlReader.Read();

            // 循环直到遇到结束元素
            while (xmlReader.NodeType != XmlNodeType.EndElement)
            {
                // 反序列化键
                TKey key = (TKey)keyXmlSerializer.Deserialize(xmlReader);

                // 反序列化值
                TValue value = (TValue)valueXmlSerializer.Deserialize(xmlReader);

                // 将键值对添加到字典中
                this.Add(key, value);
            }

            // 读取结束元素
            xmlReader.Read();
        }

        /// <summary>
        /// 将字典写入 XML。
        /// </summary>
        /// <param name="xmlWriter">XML 写入器，用于写入 XML 数据。</param>
        public void WriteXml(XmlWriter xmlWriter)
        {
            // 创建用于序列化键和值的 XmlSerializer 实例
            XmlSerializer keyXmlSerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueXmlSerializer = new XmlSerializer(typeof(TValue));

            // 遍历字典中的每个键值对
            foreach (KeyValuePair<TKey, TValue> kv in this)
            {
                // 序列化键
                keyXmlSerializer.Serialize(xmlWriter, kv.Key);

                // 序列化值
                valueXmlSerializer.Serialize(xmlWriter, kv.Value);
            }
        }
    }
}