using UnityEngine;
using ProFramework;
using UnityEngine.Events;

namespace ProFrameworkTest
{
    public class TimerTest : MonoBehaviour
    {
        void Start()
        {
            // 在 Start 方法中注册按键事件
            // 这样一旦按下特定的按键，就会触发对应的测试方法
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Time.timeScale != 0)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                TestCreateNormalTimer();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                TestCreateRealTimeTimer();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                TestRemoveTimer();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                TestResetTimer();
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                TestStartTimer();
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                TestStopTimer();
            }
        }

        void TestCreateNormalTimer()
        {
            // 创建一个普通计时器，总时间为10秒，每1秒触发一次回调
            ProTimerManager.Instance.CreateTimer("NormalTimer", 10000, false, NormalTimerOverCallback, 1000,
                NormalTimerIntervalCallback);
            ProLog.LogDebug("创建普通计时器成功。");
        }

        void TestCreateRealTimeTimer()
        {
            // 创建一个真实时间计时器，总时间为10秒，每1秒触发一次回调
            ProTimerManager.Instance.CreateTimer("RealTimeTimer", 10000, true, RealTimeTimerOverCallback, 1000,
                RealTimeTimerIntervalCallback);
            ProLog.LogDebug("创建真实时间计时器成功。");
        }

        void TestRemoveTimer()
        {
            // 移除计时器
            ProTimerManager.Instance.RemoveTimer("NormalTimer");
            ProTimerManager.Instance.RemoveTimer("RealTimeTimer");
            ProLog.LogDebug("移除计时器成功。");
        }

        void TestResetTimer()
        {
            // 重置计时器
            ProTimerManager.Instance.ResetTimer("NormalTimer");
            ProTimerManager.Instance.ResetTimer("RealTimeTimer");
            ProLog.LogDebug("重置计时器成功。");
        }

        void TestStartTimer()
        {
            // 启动计时器
            ProTimerManager.Instance.StartTimer("NormalTimer");
            ProTimerManager.Instance.StartTimer("RealTimeTimer");
            ProLog.LogDebug("启动计时器成功。");
        }

        void TestStopTimer()
        {
            // 停止计时器
            ProTimerManager.Instance.StopTimer("NormalTimer");
            ProTimerManager.Instance.StopTimer("RealTimeTimer");
            ProLog.LogDebug("停止计时器成功。");
        }

        void NormalTimerOverCallback()
        {
            ProLog.LogDebug("普通计时器结束。");
        }

        void NormalTimerIntervalCallback()
        {
            ProLog.LogDebug("普通计时器间隔回调。");
        }

        void RealTimeTimerOverCallback()
        {
            ProLog.LogDebug("真实时间计时器结束。");
        }

        void RealTimeTimerIntervalCallback()
        {
            ProLog.LogDebug("真实时间计时器间隔回调。");
        }
    }
}