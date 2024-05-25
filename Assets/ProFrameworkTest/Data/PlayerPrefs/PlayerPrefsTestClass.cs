using System.Collections.Generic;

namespace ProFrameworkTest
{
    public class PlayerPrefsTestClass
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
        public char charValue = 'A';
        public string stringValue = "Hello, PlayerPrefs!";

        // 自定义数据类型
        public PlayerPrefsTestItemClass testItemClass = new PlayerPrefsTestItemClass(123, 456);

        // 集合类型
        public int[] intArray = new[] { 1, 2, 3, 4, 5 };

        public PlayerPrefsTestItemClass[] playerPrefsTestItemClassArray = new[]
        {
            new PlayerPrefsTestItemClass(123, 456),
            new PlayerPrefsTestItemClass(789, 101112)
        };

        public List<int> intList = new List<int>() { 1, 2, 3, 4, 5 };

        public Dictionary<int, string> intStringDict = new Dictionary<int, string>()
        {
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" }
        };

        public List<PlayerPrefsTestItemClass> testItemList = new List<PlayerPrefsTestItemClass>()
        {
            new PlayerPrefsTestItemClass(111, 222),
            new PlayerPrefsTestItemClass(333, 444)
        };

        public Dictionary<int, PlayerPrefsTestItemClass> testItemDict = new Dictionary<int, PlayerPrefsTestItemClass>()
        {
            { 1001, new PlayerPrefsTestItemClass(999, 888) },
            { 1002, new PlayerPrefsTestItemClass(777, 666) }
        };
    }
}