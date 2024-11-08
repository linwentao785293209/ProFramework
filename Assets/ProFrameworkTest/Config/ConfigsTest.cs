using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ProFramework
{
    public class ConfigsTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // ���ز���ӡ��һ����������
            ProConfigManager.Instance.LoadExcelTable<ProFrameworkTestInfoContainer, ProFrameworkTestInfo>();
            ProFrameworkTestInfoContainer proFrameworkTestInfoContainer =
                ProConfigManager.Instance.GetExcelTable<ProFrameworkTestInfoContainer>();
            PrintDataContainer(proFrameworkTestInfoContainer);

            // ���ز���ӡ�ڶ�����������
            ProConfigManager.Instance.LoadExcelTable<ProFrameworkTestInfo2Container, ProFrameworkTestInfo2>();
            ProFrameworkTestInfo2Container proFrameworkTestInfo2Container =
                ProConfigManager.Instance.GetExcelTable<ProFrameworkTestInfo2Container>();
            PrintDataContainer(proFrameworkTestInfo2Container);
        }

        void PrintDataContainer<T>(T container) where T : class
        {
            // ��ȡ��������
            Type containerType = container.GetType();
            // ��ȡ dataClassDictionary �ֶ�
            FieldInfo dataClassDictionaryField = containerType.GetField("dataClassDictionary");

            if (dataClassDictionaryField != null)
            {
                // ��ȡ dataClassDictionary ��ֵ
                var dataClassDictionary = dataClassDictionaryField.GetValue(container) as IDictionary;

                if (dataClassDictionary != null)
                {
                    foreach (DictionaryEntry entry in dataClassDictionary)
                    {
                        ProLog.LogDebug($"key: {entry.Key}");
                        PrintObjectValues(entry.Value);
                    }
                }
            }
        }

        void PrintObjectValues(object obj)
        {
            // ��ȡ��������
            Type type = obj.GetType();
            // ��ȡ�����ֶ���Ϣ
            FieldInfo[] fields = type.GetFields();

            // �����ֶβ���ӡÿ���ֶε����ƺ�ֵ
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(obj);
                ProLog.LogDebug($"{field.Name}: {value}");
            }
        }
    }
}