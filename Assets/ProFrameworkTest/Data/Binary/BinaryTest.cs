using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class BinaryTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                BinaryTestClass binaryTestClass1 = new BinaryTestClass();
                binaryTestClass1.intValue = 666;
                binaryTestClass1.intStringDict.Add("6666666", "6666666");

                binaryTestClass1.stringValue = "i am shuaige!";

                // 使用 ProBinaryDataManager 存储
                ProBinaryDataManager.Instance.Save("myBinaryTestClass", binaryTestClass1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                // 使用 ProBinaryDataManager 加载
                BinaryTestClass binaryTestClass2 =
                    ProBinaryDataManager.Instance.Load<BinaryTestClass>("myBinaryTestClass");

                if (binaryTestClass2 != null)
                {
                    // 打印所有属性的值
                    PrintBinaryTestClassValues(binaryTestClass2);
                }
                else
                {
                    ProLog.LogError("加载 BinaryTestClass 失败.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ProLog.LogDebug($"{ProBinaryDataManager.Instance.PersistentDataPath}");
            }
        }

        // 打印 BinaryTestClass 中所有属性的值
        private void PrintBinaryTestClassValues(BinaryTestClass binaryTestClass)
        {
            ProLog.LogDebug("Printing values for BinaryTestClass:");

            // 打印各个类型的值
            ProLog.LogDebug($"sbyteValue: {binaryTestClass.sbyteValue}");
            ProLog.LogDebug($"intValue: {binaryTestClass.intValue}");
            ProLog.LogDebug($"shortValue: {binaryTestClass.shortValue}");
            ProLog.LogDebug($"longValue: {binaryTestClass.longValue}");
            ProLog.LogDebug($"byteValue: {binaryTestClass.byteValue}");
            ProLog.LogDebug($"uintValue: {binaryTestClass.uintValue}");
            ProLog.LogDebug($"ushortValue: {binaryTestClass.ushortValue}");
            ProLog.LogDebug($"ulongValue: {binaryTestClass.ulongValue}");
            ProLog.LogDebug($"floatValue: {binaryTestClass.floatValue}");
            ProLog.LogDebug($"doubleValue: {binaryTestClass.doubleValue}");
            ProLog.LogDebug($"decimalValue: {binaryTestClass.decimalValue}");
            ProLog.LogDebug($"boolValue: {binaryTestClass.boolValue}");
            binaryTestClass.LogBool2();
            ProLog.LogDebug($"charValue: {binaryTestClass.charValue}");
            ProLog.LogDebug($"stringValue: {binaryTestClass.stringValue}");
            ProLog.LogDebug($"testItemClass: {binaryTestClass.testItemClass}");

            // 打印数组
            ProLog.LogDebug("intArray:");
            foreach (var item in binaryTestClass.intArray)
            {
                ProLog.LogDebug($"  {item}");
            }

            // 打印数组
            ProLog.LogDebug("BinaryTestItemClassArray:");
            foreach (var item in binaryTestClass.BinaryTestItemClassArray)
            {
                ProLog.LogDebug($"  {item}");
            }

            // 打印列表
            ProLog.LogDebug("intList:");
            foreach (var item in binaryTestClass.intList)
            {
                ProLog.LogDebug($"  {item}");
            }

            // 打印字典
            ProLog.LogDebug("intStringDict:");
            foreach (var item in binaryTestClass.intStringDict)
            {
                ProLog.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }

            // 打印列表
            ProLog.LogDebug("testItemList:");
            foreach (var item in binaryTestClass.testItemList)
            {
                ProLog.LogDebug($"  {item}");
            }

            // 打印字典
            ProLog.LogDebug("testItemDict:");
            foreach (var item in binaryTestClass.testItemDict)
            {
                ProLog.LogDebug($"  Key: {item.Key}, Value: {item.Value}");
            }
        }
    }
}