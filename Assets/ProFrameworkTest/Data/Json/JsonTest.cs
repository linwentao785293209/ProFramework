using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class JsonTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                JsonTestClass jsonTestClass1 = new JsonTestClass();
                jsonTestClass1.intValue = 666;
                jsonTestClass1.intStringDict.Add("6666666", "6666666");

                // 使用 JsonUtility 存储
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.JsonUtility);
                ProJsonDataManager.Instance.Save<JsonTestClass>("myJsonTestClass_JsonUtility", jsonTestClass1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                JsonTestClass jsonTestClass1 = new JsonTestClass();
                jsonTestClass1.intValue = 999;
                jsonTestClass1.intStringDict.Add("99999999", "99999999");

                // 使用 LitJson 存储
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.LitJson);
                ProJsonDataManager.Instance.Save<JsonTestClass>("myJsonTestClass_LitJson", jsonTestClass1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                JsonTestClass jsonTestClass1 = new JsonTestClass();
                jsonTestClass1.intValue = 888;
                jsonTestClass1.intStringDict.Add("88888888", "88888888");

                // 使用 Newtonsoft.Json 存储
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.NewtonsoftJson);
                ProJsonDataManager.Instance.Save<JsonTestClass>("myJsonTestClass_NewtonsoftJson", jsonTestClass1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                // 使用 JsonUtility 加载
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.JsonUtility);
                JsonTestClass jsonTestClass2_JsonUtility =
                    ProJsonDataManager.Instance.Load<JsonTestClass>("myJsonTestClass_JsonUtility");

                if (jsonTestClass2_JsonUtility != null)
                {
                    // 打印所有属性的值
                    PrintJsonTestClassValues(jsonTestClass2_JsonUtility, "JsonUtility");
                }
                else
                {
                    ProLog.LogError("加载 JsonTestClass (JsonUtility) 失败.");
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                // 使用 LitJson 加载
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.LitJson);
                JsonTestClass jsonTestClass2_LitJson =
                    ProJsonDataManager.Instance.Load<JsonTestClass>("myJsonTestClass_LitJson");

                if (jsonTestClass2_LitJson != null)
                {
                    // 打印所有属性的值
                    PrintJsonTestClassValues(jsonTestClass2_LitJson, "LitJson");
                }
                else
                {
                    ProLog.LogError("加载 JsonTestClass (LitJson) 失败.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                // 使用 Newtonsoft.Json 加载
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.NewtonsoftJson);
                JsonTestClass jsonTestClass2_NewtonsoftJson =
                    ProJsonDataManager.Instance.Load<JsonTestClass>("myJsonTestClass_NewtonsoftJson");

                if (jsonTestClass2_NewtonsoftJson != null)
                {
                    // 打印所有属性的值
                    PrintJsonTestClassValues(jsonTestClass2_NewtonsoftJson, "NewtonsoftJson");
                }
                else
                {
                    ProLog.LogError("加载 JsonTestClass (NewtonsoftJson) 失败.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ProLog.LogDebug($"{ProJsonDataManager.Instance.PersistentDataPath}");
            }
        }

        // 打印 JsonTestClass 中所有属性的值
        private void PrintJsonTestClassValues(JsonTestClass jsonTestClass, string jsonType)
        {
            ProLog.LogDebug($"Printing values for JsonTestClass using {jsonType}:");

            // 打印各个类型的值
            ProLog.LogDebug($"sbyteValue: {jsonTestClass.sbyteValue}");
            ProLog.LogDebug($"intValue: {jsonTestClass.intValue}");
            ProLog.LogDebug($"shortValue: {jsonTestClass.shortValue}");
            ProLog.LogDebug($"longValue: {jsonTestClass.longValue}");
            ProLog.LogDebug($"byteValue: {jsonTestClass.byteValue}");
            ProLog.LogDebug($"uintValue: {jsonTestClass.uintValue}");
            ProLog.LogDebug($"ushortValue: {jsonTestClass.ushortValue}");
            ProLog.LogDebug($"ulongValue: {jsonTestClass.ulongValue}");
            ProLog.LogDebug($"floatValue: {jsonTestClass.floatValue}");
            ProLog.LogDebug($"doubleValue: {jsonTestClass.doubleValue}");
            ProLog.LogDebug($"decimalValue: {jsonTestClass.decimalValue}");
            ProLog.LogDebug($"boolValue: {jsonTestClass.boolValue}");
            jsonTestClass.LogBool2();
            ProLog.LogDebug($"charValue: {jsonTestClass.charValue}");
            ProLog.LogDebug($"stringValue: {jsonTestClass.stringValue}");
            ProLog.LogDebug($"testItemClass: {jsonTestClass.testItemClass}");

            // 打印数组
            ProLog.LogDebug("intArray:");
            foreach (var item in jsonTestClass.intArray)
            {
                ProLog.LogDebug($"  {item}");
            }

            // 打印数组
            ProLog.LogDebug("JsonTestItemClassArray:");
            foreach (var item in jsonTestClass.JsonTestItemClassArray)
            {
                ProLog.LogDebug($"  {item}");
            }

            // 打印列表
            ProLog.LogDebug("intList:");
            foreach (var item in jsonTestClass.intList)
            {
                ProLog.LogDebug($"  {item}");
            }

            // 打印字典
            ProLog.LogDebug("intStringDict:");
            foreach (var item in jsonTestClass.intStringDict)
            {
                ProLog.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }

            // 打印列表
            ProLog.LogDebug("testItemList:");
            foreach (var item in jsonTestClass.testItemList)
            {
                ProLog.LogDebug($"  {item}");
            }

            // 打印字典
            ProLog.LogDebug("testItemDict:");
            foreach (var item in jsonTestClass.testItemDict)
            {
                ProLog.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }
        }
    }
}