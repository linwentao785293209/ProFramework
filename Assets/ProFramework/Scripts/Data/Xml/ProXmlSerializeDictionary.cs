using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

namespace ProFramework
{
    public class ProXmlSerializeDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader xmlReader)
        {
            XmlSerializer keyXmlSerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueXmlSerializer = new XmlSerializer(typeof(TValue));

            xmlReader.Read();

            while (xmlReader.NodeType != XmlNodeType.EndElement)
            {
                TKey key = (TKey)keyXmlSerializer.Deserialize(xmlReader);

                TValue value = (TValue)valueXmlSerializer.Deserialize(xmlReader);

                this.Add(key, value);
            }

            xmlReader.Read();
        }

        public void WriteXml(XmlWriter xmlWriter)
        {
            XmlSerializer keyXmlSerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueXmlSerializer = new XmlSerializer(typeof(TValue));

            foreach (KeyValuePair<TKey, TValue> kv in this)
            {
                keyXmlSerializer.Serialize(xmlWriter, kv.Key);
                valueXmlSerializer.Serialize(xmlWriter, kv.Value);
            }
        }
    }
}