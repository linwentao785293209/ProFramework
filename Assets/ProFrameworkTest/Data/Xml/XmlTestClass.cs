using System.Collections.Generic;
using ProFramework;

namespace ProFrameworkTest
{
    public class XmlTestClass
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
        public string stringValue = "Hello, Xml!";

        // 自定义数据类型
        public XmlTestItemClass testItemClass = new XmlTestItemClass(123, 456);

        // 集合类型
        public int[] intArray = new[] { 1, 2, 3, 4, 5 };

        public XmlTestItemClass[] XmlTestItemClassArray = new[]
        {
            new XmlTestItemClass(123, 456),
            new XmlTestItemClass(789, 101112)
        };

        public List<int> intList = new List<int>() { 1, 2, 3, 4, 5 };

        public ProXmlSerializeDictionary<int, string> intStringDict = new ProXmlSerializeDictionary<int, string>()
        {
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" }
        };

        public List<XmlTestItemClass> testItemList = new List<XmlTestItemClass>()
        {
            new XmlTestItemClass(111, 222),
            new XmlTestItemClass(333, 444)
        };

        public ProXmlSerializeDictionary<int, XmlTestItemClass> testItemDict = new ProXmlSerializeDictionary<int, XmlTestItemClass>()
        {
            { 1001, new XmlTestItemClass(999, 888) },
            { 1002, new XmlTestItemClass(777, 666) }
        };
    
    }
}