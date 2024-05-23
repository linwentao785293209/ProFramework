using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 加密工具类，提供加密功能。
    /// </summary>
    public class ProEncryptionTool
    {
        /// <summary>
        /// 获取随机密钥。
        /// </summary>
        public static int GetRandomKey()
        {
            return Random.Range(1, 10000) + 5;
        }

        /// <summary>
        /// 加密整数数据。
        /// </summary>
        public static int LockValue(int value, int key)
        {
            value = value ^ (key % 9);
            value = value ^ 0xADAD;
            value = value ^ (1 << 5);
            value += key;
            return value;
        }

        /// <summary>
        /// 加密长整数数据。
        /// </summary>
        public static long LockValue(long value, int key)
        {
            value = value ^ (key % 9);
            value = value ^ 0xADAD;
            value = value ^ (1 << 5);
            value += key;
            return value;
        }

        /// <summary>
        /// 解密整数数据。
        /// </summary>
        public static int UnLoackValue(int value, int key)
        {
            if (value == 0)
                return value;
            value -= key;
            value = value ^ (key % 9);
            value = value ^ 0xADAD;
            value = value ^ (1 << 5);
            return value;
        }

        /// <summary>
        /// 解密长整数数据。
        /// </summary>
        public static long UnLoackValue(long value, int key)
        {
            if (value == 0)
                return value;
            value -= key;
            value = value ^ (key % 9);
            value = value ^ 0xADAD;
            value = value ^ (1 << 5);
            return value;
        }
    }
}