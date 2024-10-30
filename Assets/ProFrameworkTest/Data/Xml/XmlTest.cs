using ProFramework;
using UnityEngine;


namespace ProFrameworkTest
{
    public class XmlTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                XmlTestClass xmlTestClass1 = new XmlTestClass();
                xmlTestClass1.intValue = 250;
                ProXmlDataManager.Instance.Save<XmlTestClass>("myXmlTestClass",
                    xmlTestClass1);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                XmlTestClass xmlTestClass2 =
                    ProXmlDataManager.Instance.Load<XmlTestClass>("myXmlTestClass");

                if (xmlTestClass2 != null)
                {
                    // 打印各个类型的值
                    ProLog.LogDebug($"sbyteValue: {xmlTestClass2.sbyteValue}");
                    ProLog.LogDebug($"intValue: {xmlTestClass2.intValue}");
                    ProLog.LogDebug($"shortValue: {xmlTestClass2.shortValue}");
                    ProLog.LogDebug($"longValue: {xmlTestClass2.longValue}");
                    ProLog.LogDebug($"byteValue: {xmlTestClass2.byteValue}");
                    ProLog.LogDebug($"uintValue: {xmlTestClass2.uintValue}");
                    ProLog.LogDebug($"ushortValue: {xmlTestClass2.ushortValue}");
                    ProLog.LogDebug($"ulongValue: {xmlTestClass2.ulongValue}");
                    ProLog.LogDebug($"floatValue: {xmlTestClass2.floatValue}");
                    ProLog.LogDebug($"doubleValue: {xmlTestClass2.doubleValue}");
                    ProLog.LogDebug($"decimalValue: {xmlTestClass2.decimalValue}");
                    ProLog.LogDebug($"boolValue: {xmlTestClass2.boolValue}");
                    ProLog.LogDebug($"charValue: {xmlTestClass2.charValue}");
                    ProLog.LogDebug($"stringValue: {xmlTestClass2.stringValue}");
                    ProLog.LogDebug($"testItemClass: {xmlTestClass2.testItemClass}");

                    // 打印数组
                    ProLog.LogDebug("intArray:");
                    foreach (var item in xmlTestClass2.intArray)
                    {
                        ProLog.LogDebug($"  {item}");
                    }

                    // 打印数组
                    ProLog.LogDebug("XmlTestItemClassArray:");
                    foreach (var item in xmlTestClass2.XmlTestItemClassArray)
                    {
                        ProLog.LogDebug($"  {item}");
                    }

                    // 打印列表
                    ProLog.LogDebug("intList:");
                    foreach (var item in xmlTestClass2.intList)
                    {
                        ProLog.LogDebug($"  {item}");
                    }

                    // 打印字典
                    ProLog.LogDebug("intStringDict:");
                    foreach (var item in xmlTestClass2.intStringDict)
                    {
                        ProLog.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
                    }

                    // 打印列表
                    ProLog.LogDebug("testItemList:");
                    foreach (var item in xmlTestClass2.testItemList)
                    {
                        ProLog.LogDebug($"  {item}");
                    }

                    // 打印字典
                    ProLog.LogDebug("testItemDict:");
                    foreach (var item in xmlTestClass2.testItemDict)
                    {
                        ProLog.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
                    }
                }
                else
                {
                    ProLog.LogError("加载 XmlTestClass 失败.");
                }

                ProLog.LogDebug($"{ProXmlDataManager.Instance.PersistentDataPath}");
            }
        }
    }
}