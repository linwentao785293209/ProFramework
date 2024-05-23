using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class TextTest : MonoBehaviour
    {
        void Start()
        {
            // 测试字符串拆分相关方法
            TestSplitStr();
            // 测试数字转字符串相关方法
            TestNumberToString();
            // 测试时间转换相关方法
            TestTimeConversion();
        }

        // 测试字符串拆分相关方法
        void TestSplitStr()
        {
            string testStr = "1,2,3,4,5";
            // 测试使用不同的拆分字符类型
            TestSplitStrWithDifferentTypes(testStr);
        }

        // 测试使用不同的拆分字符类型
        void TestSplitStrWithDifferentTypes(string testStr)
        {
            for (int i = 1; i <= 7; i++)
            {
                string[] splitResult = ProFramework.ProTextTool.SplitStr(testStr, i);
                foreach (var str in splitResult)
                {
                    ProLog.LogDebug($"拆分类型 {i}: {str}");
                }
            }
        }

        // 测试数字转字符串相关方法
        void TestNumberToString()
        {
            int testNum = 123456789;
            // 测试将数字转换为字符串
            TestGetNumStr(testNum);
            // 测试将浮点数转换为保留指定位数小数的字符串
            TestGetDecimalStr(123.456f, 2);
            // 测试将较大较长的数转换为字符串
            TestGetBigDataToString(testNum);
        }

        // 测试将数字转换为字符串
        void TestGetNumStr(int testNum)
        {
            string resultStr = ProFramework.ProTextTool.GetNumStr(testNum, 5);
            ProLog.LogDebug($"数字转字符串: {resultStr}");
        }

        // 测试将浮点数转换为保留指定位数小数的字符串
        void TestGetDecimalStr(float testFloat, int decimalPlaces)
        {
            string resultStr = ProFramework.ProTextTool.GetDecimalStr(testFloat, decimalPlaces);
            ProLog.LogDebug($"浮点数转字符串（保留 {decimalPlaces} 位小数）: {resultStr}");
        }

        // 测试将较大较长的数转换为字符串
        void TestGetBigDataToString(int testNum)
        {
            string resultStr = ProFramework.ProTextTool.GetBigDataToString(testNum);
            ProLog.LogDebug($"大数值转字符串: {resultStr}");
        }

        // 测试时间转换相关方法
        void TestTimeConversion()
        {
            int testSeconds = 123456789;
            // 测试将秒数转换为时分秒格式
            TestSecondToHMS(testSeconds);
            // 测试将秒数转换为00:00:00格式
            TestSecondToHMS2(testSeconds);
        }

        // 测试将秒数转换为时分秒格式
        void TestSecondToHMS(int testSeconds)
        {
            string resultHMS = ProFramework.ProTextTool.SecondToHMS(testSeconds);
            ProLog.LogDebug($"秒数转为时分秒格式: {resultHMS}");
        }

        // 测试将秒数转换为00:00:00格式
        void TestSecondToHMS2(int testSeconds)
        {
            string resultHMS2 = ProFramework.ProTextTool.SecondToHMS2(testSeconds);
            ProLog.LogDebug($"秒数转为00:00:00格式: {resultHMS2}");
        }
    }
}