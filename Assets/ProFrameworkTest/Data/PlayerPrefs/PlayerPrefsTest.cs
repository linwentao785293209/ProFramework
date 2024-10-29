using System;
using System.Linq;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class PlayerPrefsTest : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayerPrefsTestClass playerPrefsTestClass1 = new PlayerPrefsTestClass();
                ProPlayerPrefsDataManager.Instance.Save<PlayerPrefsTestClass>("myPlayerPrefsTestClass", playerPrefsTestClass1);
                ProLog.LogDebug("Saved PlayerPrefsTestClass: " + playerPrefsTestClass1);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                PlayerPrefsTestClass playerPrefsTestClass2 = ProPlayerPrefsDataManager.Instance.Load<PlayerPrefsTestClass>("myPlayerPrefsTestClass");
                if (playerPrefsTestClass2 != null)
                {
                    ProLog.LogDebug("Loaded PlayerPrefsTestClass:");
                    ProLog.LogDebug("sbyteValue: " + playerPrefsTestClass2.sbyteValue);
                    ProLog.LogDebug("intValue: " + playerPrefsTestClass2.intValue);
                    ProLog.LogDebug("shortValue: " + playerPrefsTestClass2.shortValue);
                    ProLog.LogDebug("longValue: " + playerPrefsTestClass2.longValue);
                    ProLog.LogDebug("byteValue: " + playerPrefsTestClass2.byteValue);
                    ProLog.LogDebug("uintValue: " + playerPrefsTestClass2.uintValue);
                    ProLog.LogDebug("ushortValue: " + playerPrefsTestClass2.ushortValue);
                    ProLog.LogDebug("ulongValue: " + playerPrefsTestClass2.ulongValue);
                    ProLog.LogDebug("floatValue: " + playerPrefsTestClass2.floatValue);
                    ProLog.LogDebug("doubleValue: " + playerPrefsTestClass2.doubleValue);
                    ProLog.LogDebug("decimalValue: " + playerPrefsTestClass2.decimalValue);
                    ProLog.LogDebug("boolValue: " + playerPrefsTestClass2.boolValue);
                    ProLog.LogDebug("charValue: " + playerPrefsTestClass2.charValue);
                    ProLog.LogDebug("stringValue: " + playerPrefsTestClass2.stringValue);
                    ProLog.LogDebug("testItemClass: " + playerPrefsTestClass2.testItemClass);
                    ProLog.LogDebug("intArray: " + string.Join(", ", playerPrefsTestClass2.intArray));
                    // 遍历数组并打印
                    foreach (var item in playerPrefsTestClass2.playerPrefsTestItemClassArray)
                    {
                        ProLog.LogDebug("playerPrefsTestItemClassArray item: id = " + item.id + ", num = " + item.num);
                    }
                    ProLog.LogDebug("intList: " + string.Join(", ", playerPrefsTestClass2.intList));
                    ProLog.LogDebug("intStringDict: " + string.Join(", ", playerPrefsTestClass2.intStringDict.Select(kv => kv.Key + ": " + kv.Value)));
                    
                    
                    // 遍历并打印列表
                    foreach (var item in playerPrefsTestClass2.testItemList)
                    {
                        ProLog.LogDebug("testItemList item: id = " + item.id + ", num = " + item.num);
                    }
                    
                    // 遍历并打印字典
                    foreach (var kv in playerPrefsTestClass2.testItemDict)
                    {
                        ProLog.LogDebug($"testItemDict key = {kv.Key}, value: id = {kv.Value.id}, num = {kv.Value.num}");
                    }
                }
                else
                {
                    ProLog.LogDebug("Failed to load PlayerPrefsTestClass.");
                }
            }
        }
    }
}
