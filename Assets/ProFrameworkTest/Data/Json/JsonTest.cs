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

                // ʹ�� JsonUtility �洢
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.JsonUtility);
                ProJsonDataManager.Instance.Save<JsonTestClass>("myJsonTestClass_JsonUtility", jsonTestClass1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                JsonTestClass jsonTestClass1 = new JsonTestClass();
                jsonTestClass1.intValue = 999;
                jsonTestClass1.intStringDict.Add("99999999", "99999999");

                // ʹ�� LitJson �洢
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.LitJson);
                ProJsonDataManager.Instance.Save<JsonTestClass>("myJsonTestClass_LitJson", jsonTestClass1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                JsonTestClass jsonTestClass1 = new JsonTestClass();
                jsonTestClass1.intValue = 888;
                jsonTestClass1.intStringDict.Add("88888888", "88888888");

                // ʹ�� Newtonsoft.Json �洢
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.NewtonsoftJson);
                ProJsonDataManager.Instance.Save<JsonTestClass>("myJsonTestClass_NewtonsoftJson", jsonTestClass1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                // ʹ�� JsonUtility ����
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.JsonUtility);
                JsonTestClass jsonTestClass2_JsonUtility =
                    ProJsonDataManager.Instance.Load<JsonTestClass>("myJsonTestClass_JsonUtility");

                if (jsonTestClass2_JsonUtility != null)
                {
                    // ��ӡ�������Ե�ֵ
                    PrintJsonTestClassValues(jsonTestClass2_JsonUtility, "JsonUtility");
                }
                else
                {
                    ProLog.LogError("���� JsonTestClass (JsonUtility) ʧ��.");
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                // ʹ�� LitJson ����
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.LitJson);
                JsonTestClass jsonTestClass2_LitJson =
                    ProJsonDataManager.Instance.Load<JsonTestClass>("myJsonTestClass_LitJson");

                if (jsonTestClass2_LitJson != null)
                {
                    // ��ӡ�������Ե�ֵ
                    PrintJsonTestClassValues(jsonTestClass2_LitJson, "LitJson");
                }
                else
                {
                    ProLog.LogError("���� JsonTestClass (LitJson) ʧ��.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                // ʹ�� Newtonsoft.Json ����
                ProJsonDataManager.Instance.SetJsonType(EProJsonType.NewtonsoftJson);
                JsonTestClass jsonTestClass2_NewtonsoftJson =
                    ProJsonDataManager.Instance.Load<JsonTestClass>("myJsonTestClass_NewtonsoftJson");

                if (jsonTestClass2_NewtonsoftJson != null)
                {
                    // ��ӡ�������Ե�ֵ
                    PrintJsonTestClassValues(jsonTestClass2_NewtonsoftJson, "NewtonsoftJson");
                }
                else
                {
                    ProLog.LogError("���� JsonTestClass (NewtonsoftJson) ʧ��.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ProLog.LogDebug($"{ProJsonDataManager.Instance.PersistentDataPath}");
            }
        }

        // ��ӡ JsonTestClass ���������Ե�ֵ
        private void PrintJsonTestClassValues(JsonTestClass jsonTestClass, string jsonType)
        {
            ProLog.LogDebug($"Printing values for JsonTestClass using {jsonType}:");

            // ��ӡ�������͵�ֵ
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

            // ��ӡ����
            ProLog.LogDebug("intArray:");
            foreach (var item in jsonTestClass.intArray)
            {
                ProLog.LogDebug($"  {item}");
            }

            // ��ӡ����
            ProLog.LogDebug("JsonTestItemClassArray:");
            foreach (var item in jsonTestClass.JsonTestItemClassArray)
            {
                ProLog.LogDebug($"  {item}");
            }

            // ��ӡ�б�
            ProLog.LogDebug("intList:");
            foreach (var item in jsonTestClass.intList)
            {
                ProLog.LogDebug($"  {item}");
            }

            // ��ӡ�ֵ�
            ProLog.LogDebug("intStringDict:");
            foreach (var item in jsonTestClass.intStringDict)
            {
                ProLog.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }

            // ��ӡ�б�
            ProLog.LogDebug("testItemList:");
            foreach (var item in jsonTestClass.testItemList)
            {
                ProLog.LogDebug($"  {item}");
            }

            // ��ӡ�ֵ�
            ProLog.LogDebug("testItemDict:");
            foreach (var item in jsonTestClass.testItemDict)
            {
                ProLog.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }
        }
    }
}