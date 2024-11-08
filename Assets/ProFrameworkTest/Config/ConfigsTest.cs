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
            // 加载并打印第一个数据容器
            ProConfigManager.Instance.LoadExcelTable<ProFrameworkTestInfoContainer, ProFrameworkTestInfo>();
            ProFrameworkTestInfoContainer proFrameworkTestInfoContainer =
                ProConfigManager.Instance.GetExcelTable<ProFrameworkTestInfoContainer>();
            PrintDataContainer(proFrameworkTestInfoContainer);

            // 加载并打印第二个数据容器
            ProConfigManager.Instance.LoadExcelTable<ProFrameworkTestInfo2Container, ProFrameworkTestInfo2>();
            ProFrameworkTestInfo2Container proFrameworkTestInfo2Container =
                ProConfigManager.Instance.GetExcelTable<ProFrameworkTestInfo2Container>();
            PrintDataContainer(proFrameworkTestInfo2Container);
        }

        void PrintDataContainer<T>(T container) where T : class
        {
            // 获取容器类型
            Type containerType = container.GetType();
            // 获取 dataClassDictionary 字段
            FieldInfo dataClassDictionaryField = containerType.GetField("dataClassDictionary");

            if (dataClassDictionaryField != null)
            {
                // 获取 dataClassDictionary 的值
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
            // 获取对象类型
            Type type = obj.GetType();
            // 获取所有字段信息
            FieldInfo[] fields = type.GetFields();

            // 遍历字段并打印每个字段的名称和值
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(obj);
                ProLog.LogDebug($"{field.Name}: {value}");
            }
        }
    }
}