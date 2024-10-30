using System.Collections.Generic;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    // JsonUtility需要加特性
    [System.Serializable] 
    public class JsonTestClass
    {
        // 有符号类型
        public sbyte sbyteValue = -50;
        public int intValue = -123456789;
        public short shortValue = -30000;
        public long longValue = -98765432101234L;

        // 无符号类型
        public byte byteValue = 100;
        public uint uintValue = 4294967295;
        public ushort ushortValue = 60000;
        public ulong ulongValue = 18446744073709551615;

        // 浮点数
        public float floatValue = 3.14f;
        public double doubleValue = 1234567890.123456789;
        public decimal decimalValue = 1234567890123456789012345678.12345678m;

        // 特殊类型
        public bool boolValue = true;

        // JsonUtility私有变量需要加特性
        [SerializeField]
        private bool boolValue2 = false;
        public char charValue = 'A';
        public string stringValue = "Hello, Json!";

        // 自定义数据类型
        public JsonTestItemClass testItemClass = new JsonTestItemClass(123, 456);

        // 集合类型
        public int[] intArray = new[] { 1, 2, 3, 4, 5 };

        public JsonTestItemClass[] JsonTestItemClassArray = new[]
        {
            new JsonTestItemClass(123, 456),
            new JsonTestItemClass(789, 101112)
        };

        public List<int> intList = new List<int>() { 1, 2, 3, 4, 5 };

        //只支持字符串类型的字典
        public Dictionary<string, string> intStringDict = new Dictionary<string, string>()
        {
            { "1", "One" },
            { "2", "Two" },
            { "3", "Three" }
        };

        public List<JsonTestItemClass> testItemList = new List<JsonTestItemClass>()
        {
            new JsonTestItemClass(111, 222),
            new JsonTestItemClass(333, 444)
        };

        //只支持字符串类型的字典
        public Dictionary<string, JsonTestItemClass> testItemDict = new Dictionary<string, JsonTestItemClass>()
        {
            { "1001", new JsonTestItemClass(999, 888) },
            { "1002", new JsonTestItemClass(777, 666) }
        };

        public void LogBool2()
        {
            ProLog.LogDebug($"bool2Value: {boolValue2}");
        }
    }
}