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
                    ProLog.LogDebug($"{CreateRandomStr()}",$"{CreateRandomStr()}",$"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    ProLog.LogInfo($"{CreateRandomStr()}",$"{CreateRandomStr()}",$"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    ProLog.LogWarning($"{CreateRandomStr()}",$"{CreateRandomStr()}",$"{CreateRandomStr()}");
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    ProLog.LogError($"{CreateRandomStr()}",$"{CreateRandomStr()}",$"{CreateRandomStr()}");
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