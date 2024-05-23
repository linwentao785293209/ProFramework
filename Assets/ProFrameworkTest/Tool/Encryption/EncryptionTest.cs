using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class EncryptionTest : MonoBehaviour
    {
        int originalInt;
        long originalLong;
        int key;
        int encryptedInt;
        long encryptedLong;
        int decryptedInt;
        long decryptedLong;

        void Start()
        {
            TestEncryption();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // 获取随机密钥
                key = ProEncryptionTool.GetRandomKey();
                ProLog.LogDebug("随机密钥：" + key);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                // 加密数据
                encryptedInt = ProEncryptionTool.LockValue(originalInt, key);
                encryptedLong = ProEncryptionTool.LockValue(originalLong, key);
                ProLog.LogDebug("加密后整数值：" + encryptedInt);
                ProLog.LogDebug("加密后长整数值：" + encryptedLong);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                // 解密数据
                decryptedInt = ProEncryptionTool.UnLoackValue(encryptedInt, key);
                decryptedLong = ProEncryptionTool.UnLoackValue(encryptedLong, key);
                ProLog.LogDebug("解密后整数值：" + decryptedInt);
                ProLog.LogDebug("解密后长整数值：" + decryptedLong);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                // 重新返回
                ResetValues();
                TestEncryption();
            }
        }

        /// <summary>
        /// 测试加密和解密功能。
        /// </summary>
        private void TestEncryption()
        {
            // 生成新的随机原始数据
            originalInt = Random.Range(1, 1000);
            originalLong = Random.Range(1000, 1000000);
            ProLog.LogDebug("原始整数值：" + originalInt);
            ProLog.LogDebug("原始长整数值：" + originalLong);
        }

        /// <summary>
        /// 重置加密解密相关值。
        /// </summary>
        private void ResetValues()
        {
            encryptedInt = 0;
            encryptedLong = 0;
            decryptedInt = 0;
            decryptedLong = 0;
        }
    }
}
