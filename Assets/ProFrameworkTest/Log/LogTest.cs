using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class LogTest : MonoBehaviour
    {
        void Update()
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ProLog.LogDebug($"{CreateRandomStr()}", $"{CreateRandomStr()}", $"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    ProLog.LogInfo($"{CreateRandomStr()}", $"{CreateRandomStr()}", $"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    ProLog.LogWarning($"{CreateRandomStr()}", $"{CreateRandomStr()}", $"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    ProLog.LogError($"{CreateRandomStr()}", $"{CreateRandomStr()}", $"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    ProLog.SetLogConfig<ProLogDebugConfig>();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    ProLog.SetLogConfig<ProLogInfoConfig>();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    ProLog.SetLogConfig<ProLogWarningConfig>();
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    ProLog.SetLogConfig<ProLogErrorConfig>();
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    ProLog.SetLogConfig<ProLogCloseConfig>();
                }
            }
        }

        string CreateRandomStr()
        {
            int randomNum = Random.Range(1, 20);
            string resultStr = "";
            for (int j = 0; j < randomNum; j++)
            {
                char randomChar = (char)Random.Range('A', 'Z');
                resultStr += randomChar;
            }

            return resultStr;
        }
    }
}