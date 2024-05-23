using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// 用于处理字符串的一些公共功能的
    /// </summary>
    public class ProTextTool
    {
        // StringBuilder对象，用于拼接字符串
        private static StringBuilder resultStr = new StringBuilder("");

        #region 字符串拆分相关

        /// <summary>
        /// 拆分字符串 返回字符串数组
        /// </summary>
        /// <param name="str">想要被拆分的字符串</param>
        /// <param name="type">拆分字符类型： 1-; 2-, 3-% 4-: 5-空格 6-| 7-_ </param>
        /// <returns>拆分后的字符串数组</returns>
        public static string[] SplitStr(string str, int type = 1)
        {
            // 如果字符串为空，则返回空数组
            if (str == "")
                return new string[0];
            string newStr = str;
            if (type == 1)
            {
                // 为了避免英文符号填成了中文符号，进行替换
                while (newStr.IndexOf("；") != -1)
                    newStr = newStr.Replace("；", ";");
                return newStr.Split(';');
            }
            else if (type == 2)
            {
                // 为了避免英文符号填成了中文符号，进行替换
                while (newStr.IndexOf("，") != -1)
                    newStr = newStr.Replace("，", ",");
                return newStr.Split(',');
            }
            else if (type == 3)
            {
                return newStr.Split('%');
            }
            else if (type == 4)
            {
                // 为了避免英文符号填成了中文符号，进行替换
                while (newStr.IndexOf("：") != -1)
                    newStr = newStr.Replace("：", ":");
                return newStr.Split(':');
            }
            else if (type == 5)
            {
                return newStr.Split(' ');
            }
            else if (type == 6)
            {
                return newStr.Split('|');
            }
            else if (type == 7)
            {
                return newStr.Split('_');
            }

            return new string[0];
        }

        /// <summary>
        /// 拆分字符串 返回整形数组
        /// </summary>
        /// <param name="str">想要被拆分的字符串</param>
        /// <param name="type">拆分字符类型： 1-; 2-, 3-% 4-: 5-空格 6-| 7-_ </param>
        /// <returns>拆分后的整型数组</returns>
        public static int[] SplitStrToIntArr(string str, int type = 1)
        {
            // 得到拆分后的字符串数组
            string[] strs = SplitStr(str, type);
            // 如果数组长度为0，返回空数组
            if (strs.Length == 0)
                return new int[0];
            // 将字符串数组转换为整型数组
            return Array.ConvertAll<string, int>(strs, (str) => { return int.Parse(str); });
        }

        /// <summary>
        /// 专门用来拆分多组键值对形式的数据的 以int返回
        /// </summary>
        /// <param name="str">待拆分的字符串</param>
        /// <param name="typeOne">组间分隔符  1-; 2-, 3-% 4-: 5-空格 6-| 7-_ </param>
        /// <param name="typeTwo">键值对分隔符 1-; 2-, 3-% 4-: 5-空格 6-| 7-_ </param>
        /// <param name="callBack">回调函数</param>
        public static void SplitStrToIntArrTwice(string str, int typeOne, int typeTwo, UnityAction<int, int> callBack)
        {
            string[] strs = SplitStr(str, typeOne);
            // 如果数组长度为0，直接返回
            if (strs.Length == 0)
                return;
            int[] ints;
            for (int i = 0; i < strs.Length; i++)
            {
                // 拆分单个道具的ID和数量信息
                ints = SplitStrToIntArr(strs[i], typeTwo);
                // 如果数组长度为0，跳过当前循环
                if (ints.Length == 0)
                    continue;
                callBack.Invoke(ints[0], ints[1]);
            }
        }

        /// <summary>
        /// 专门用来拆分多组键值对形式的数据的 以string返回
        /// </summary>
        /// <param name="str">待拆分的字符串</param>
        /// <param name="typeOne">组间分隔符 1-; 2-, 3-% 4-: 5-空格 6-| 7-_ </param>
        /// <param name="typeTwo">键值对分隔符  1-; 2-, 3-% 4-: 5-空格 6-| 7-_ </param>
        /// <param name="callBack">回调函数</param>
        public static void SplitStrTwice(string str, int typeOne, int typeTwo, UnityAction<string, string> callBack)
        {
            string[] strs = SplitStr(str, typeOne);
            // 如果数组长度为0，直接返回
            if (strs.Length == 0)
                return;
            string[] strs2;
            for (int i = 0; i < strs.Length; i++)
            {
                // 拆分单个道具的ID和数量信息
                strs2 = SplitStr(strs[i], typeTwo);
                // 如果数组长度为0，跳过当前循环
                if (strs2.Length == 0)
                    continue;
                callBack.Invoke(strs2[0], strs2[1]);
            }
        }

        #endregion

        #region 数字转字符串相关

        /// <summary>
        /// 得到指定长度的数字转字符串内容，如果长度不够会在前面补0，如果长度超出，会保留原始数值
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="len">长度</param>
        /// <returns>转换后的字符串</returns>
        public static string GetNumStr(int value, int len)
        {
            // ToString方法中传入一个 Dn 的字符串，代表想要将数字转换为长度为n的字符串，如果长度不够，会在前面补0。返回转换后的字符串。
            return value.ToString($"D{len}");
        }

        /// <summary>
        /// 让指定浮点数保留小数点后n位
        /// </summary>
        /// <param name="value">具体的浮点数</param>
        /// <param name="len">保留小数点后n位</param>
        /// <returns>转换后的字符串</returns>
        public static string GetDecimalStr(float value, int len)
        {
            // ToString方法中传入一个 Fn 的字符串，代表想要保留小数点后几位小数
            return value.ToString($"F{len}");
        }

        /// <summary>
        /// 将较大较长的数 转换为字符串
        /// </summary>
        /// <param name="num">具体数值</param>
        /// <returns>n亿n千万 或 n万n千 或 1000 3434 234</returns>
        public static string GetBigDataToString(int num)
        {
            // 如果大于1亿，显示n亿n千万；如果大于1万，显示n万n千；否则直接显示数值本身
            if (num >= 100000000)
            {
                return BigDataChange(num, 100000000, "亿", "千万");
            }
            else if (num >= 10000)
            {
                return BigDataChange(num, 10000, "万", "千");
            }
            else
                return num.ToString();
        }

        /// <summary>
        /// 把大数据转换成对应的字符串拼接
        /// </summary>
        /// <param name="num">数值</param>
        /// <param name="company">分割单位 可以填 100000000、10000</param>
        /// <param name="bigCompany">大单位 亿、万</param>
        /// <param name="littltCompany">小单位 万、千</param>
        /// <returns>拼接后的字符串</returns>
        private static string BigDataChange(int num, int company, string bigCompany, string littltCompany)
        {
            resultStr.Clear();
            // 有几亿、几万
            resultStr.Append(num / company);
            resultStr.Append(bigCompany);
            // 有几千万、几千
            int tmpNum = num % company;
            // 看有几千万、几千
            tmpNum /= (company / 10);
            // 如果算出来不为0
            if (tmpNum != 0)
            {
                resultStr.Append(tmpNum);
                resultStr.Append(littltCompany);
            }

            return resultStr.ToString();
        }

        #endregion

        #region 时间转换相关

        /// <summary>
        /// 秒转时分秒格式 其中时分秒可以自己传
        /// </summary>
        /// <param name="s">秒数</param>
        /// <param name="egZero">是否忽略0</param>
        /// <param name="isKeepLen">是否保留至少2位</param>
        /// <param name="hourStr">小时的拼接字符</param>
        /// <param name="minuteStr">分钟的拼接字符</param>
        /// <param name="secondStr">秒的拼接字符</param>
        /// <returns>转换后的时分秒格式字符串</returns>
        public static string SecondToHMS(int s, bool egZero = false, bool isKeepLen = false, string hourStr = "时",
            string minuteStr = "分", string secondStr = "秒")
        {
            // 时间不会有负数，如果发现是负数直接归0
            if (s < 0)
                s = 0;
            // 计算小时
            int hour = s / 3600;
            // 计算分钟
            // 除去小时后的剩余秒
            int second = s % 3600;
            // 剩余秒转为分钟数
            int minute = second / 60;
            // 计算秒
            second = s % 60;
            // 拼接
            resultStr.Clear();
            // 如果小时不为0或者不忽略0
            if (hour != 0 || !egZero)
            {
                if (isKeepLen)
                {
                    resultStr.Append(GetNumStr(hour, 2)); // 具体几个小时
                }
                else
                {
                    resultStr.Append(hour); // 具体几个小时
                }

                resultStr.Append(hourStr);
            }

            // 如果分钟不为0或者不忽略0或者小时不为0
            if (minute != 0 || !egZero || hour != 0)
            {
                if (isKeepLen)
                {
                    resultStr.Append(GetNumStr(minute, 2)); // 具体几分钟
                }
                else
                {
                    resultStr.Append(minute); // 具体几分钟
                }


                resultStr.Append(minuteStr);
            }

            // 如果秒不为0或者不忽略0或者小时和分钟不为0
            if (second != 0 || !egZero || hour != 0 || minute != 0)
            {
                if (isKeepLen)
                {
                    resultStr.Append(GetNumStr(second, 2)); // 具体多少秒
                }
                else
                {
                    resultStr.Append(second); // 具体多少秒
                }

                resultStr.Append(secondStr);
            }

            // 如果传入的参数是0秒时
            if (resultStr.Length == 0)
            {
                resultStr.Append(0);
                resultStr.Append(secondStr);
            }

            return resultStr.ToString();
        }

        /// <summary>
        /// 秒转00:00:00格式
        /// </summary>
        /// <param name="s">秒数</param>
        /// <param name="egZero">是否忽略0</param>
        /// <returns>转换后的00:00:00格式字符串</returns>
        public static string SecondToHMS2(int s, bool egZero = false)
        {
            return SecondToHMS(s, egZero, true, ":", ":", "");
        }

        #endregion
    }
}