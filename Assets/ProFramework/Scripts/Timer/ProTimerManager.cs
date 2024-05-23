using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// 计时器管理器 主要用于开启、停止、重置等等操作来管理计时器
    /// </summary>
    public class ProTimerManager : ProSingletonInSystem<ProTimerManager>
    {
        /// <summary>
        /// 用于存储管理所有计时器的字典容器
        /// </summary>
        private Dictionary<object, ProTimer> _normalTimerDictionary = new Dictionary<object, ProTimer>();

        /// <summary>
        /// 用于存储管理所有计时器的字典容器（不受Time.timeScale影响的计时器）
        /// </summary>
        private Dictionary<object, ProTimer> _realTimerDictionary = new Dictionary<object, ProTimer>();

        /// <summary>
        /// 待移除列表
        /// </summary>
        private List<ProTimer> _deletedTimerList = new List<ProTimer>();

        //避免内存浪费，将其声明为成员变量
        private WaitForSecondsRealtime _waitForSecondsRealtime;
        private WaitForSeconds _waitForSeconds;

        private Coroutine _normalTimerCoroutine;
        private Coroutine _realTimerCoroutine;

        /// <summary>
        /// 计时器管理器中的唯一计时用的协同程序的间隔时间
        /// </summary>
        private static float IntervalTime => Time.fixedDeltaTime;

        private ProTimerManager()
        {
            //默认计时器是开启的
            Start();
        }

        //开启计时器管理器的方法
        private void Start()
        {
            _waitForSecondsRealtime = new WaitForSecondsRealtime(IntervalTime);
            _waitForSeconds = new WaitForSeconds(IntervalTime);

            _normalTimerCoroutine = ProMonoManager.Instance.StartCoroutine(StartTiming(false, _normalTimerDictionary));
            _realTimerCoroutine = ProMonoManager.Instance.StartCoroutine(StartTiming(true, _realTimerDictionary));
        }

        //关闭计时器管理器的方法
        public void Stop()
        {
            if (_normalTimerCoroutine != null)
            {
                ProMonoManager.Instance.StopCoroutine(_normalTimerCoroutine);
                _normalTimerCoroutine = null;
            }

            if (_realTimerCoroutine != null)
            {
                ProMonoManager.Instance.StopCoroutine(_realTimerCoroutine);
                _realTimerCoroutine = null;
            }
        }

        /// <summary>
        /// 开启计时器协程
        /// </summary>
        /// <param name="isRealTime"></param>
        /// <param name="timerDictionary"></param>
        /// <returns></returns>
        private IEnumerator StartTiming(bool isRealTime, Dictionary<object, ProTimer> timerDictionary)
        {
            while (true)
            {
                if (isRealTime)
                {
                    yield return _waitForSecondsRealtime;
                }
                else
                {
                    yield return _waitForSeconds;
                }


                //遍历所有的计时器 进行数据更新
                foreach (var kvp in timerDictionary)
                {
                    var timer = kvp.Value;

                    if (!timer.isRunning)
                        continue;

                    //判断计时器是否有间隔时间执行的需求
                    if (timer.callBack != null)
                    {
                        //减去100毫秒
                        timer.nowIntervalTime -= (int)(IntervalTime * 1000);
                        //满足一次间隔时间执行
                        if (timer.nowIntervalTime <= 0)
                        {
                            //间隔一定时间 执行一次回调
                            timer.callBack.Invoke();
                            //重置间隔时间
                            timer.nowIntervalTime = timer.maxIntervalTime;
                        }
                    }

                    //总的时间更新
                    timer.nowAllTime -= (int)(IntervalTime * 1000);

                    //计时时间到 需要执行完成回调函数
                    if (timer.nowAllTime <= 0)
                    {
                        timer.overCallBack.Invoke();
                        _deletedTimerList.Add(timer);
                    }
                }

                //移除待移除列表中的数据
                foreach (var timer in _deletedTimerList)
                {
                    //从字典中移除
                    timerDictionary.Remove(timer.keyID);

                    //放入缓存池中
                    ProSystemObjectPoolManager.Instance.Push(timer);
                }

                //移除结束后 清空列表
                _deletedTimerList.Clear();
            }
        }

        /// <summary>
        /// 创建单个计时器
        /// </summary>
        /// <param name="keyID">唯一key</param>
        /// <param name="allTime">总的时间 毫秒 1s=1000ms</param>
        /// <param name="isRealTime">是否受Time.timeScale影响</param>
        /// <param name="overCallBack">总时间结束回调</param>
        /// <param name="intervalTime">间隔计时时间 毫秒 1s=1000ms</param>
        /// <param name="callBack">间隔计时时间结束 回调</param>
        public void CreateTimer(object keyID, int allTime, bool isRealTime, UnityAction overCallBack,
            int intervalTime = 0, UnityAction callBack = null)
        {
            if (keyID == null)
            {
                throw new ArgumentNullException(nameof(keyID));
            }


            //从缓存池取出对应的计时器
            ProTimer timerItem = ProSystemObjectPoolManager.Instance.Get<ProTimer>();

            //初始化数据
            timerItem.Init(keyID, allTime, overCallBack, intervalTime, callBack);

            //记录到字典中 进行数据更新
            var timerDictionary = isRealTime ? _realTimerDictionary : _normalTimerDictionary;
            if (timerDictionary.ContainsKey(keyID))
            {
                ProLog.LogWarning($"Timer with keyID {keyID} already exists.");
                return;
            }

            timerDictionary.Add(keyID, timerItem);
        }

        /// <summary>
        /// 移除单个计时器
        /// </summary>
        /// <param name="keyID">唯一key</param>
        public void RemoveTimer(object keyID)
        {
            if (TryGetTimer(keyID, out var timer, out var timerDictionary))
            {
                //移除对应id计时器 放入缓存池
                ProSystemObjectPoolManager.Instance.Push(timer);

                //从字典中移除
                timerDictionary.Remove(keyID);
            }
        }

        /// <summary>
        /// 重置单个计时器
        /// </summary>
        /// <param name="keyID">计时器唯一ID</param>
        public void ResetTimer(object keyID)
        {
            if (TryGetTimer(keyID, out var timer, out _))
            {
                timer.ResetTimer();
            }
        }

        /// <summary>
        /// 开启单个计时器 主要用于暂停后重新开始
        /// </summary>
        /// <param name="keyID">计时器唯一ID</param>
        public void StartTimer(object keyID)
        {
            if (TryGetTimer(keyID, out var timer, out _))
            {
                timer.isRunning = true;
            }
        }

        /// <summary>
        /// 停止单个计时器 主要用于暂停
        /// </summary>
        /// <param name="keyID">计时器唯一ID</param>
        public void StopTimer(object keyID)
        {
            if (TryGetTimer(keyID, out var timer, out _))
            {
                timer.isRunning = false;
            }
        }

        /// <summary>
        /// 尝试根据唯一ID获取计时器对象。
        /// </summary>
        /// <param name="keyID">计时器的唯一ID。</param>
        /// <param name="timer">输出参数，返回找到的计时器对象。</param>
        /// <param name="timerDictionary">输出参数，返回计时器所在的字典。</param>
        /// <returns>如果找到计时器，返回true；否则返回false。</returns>
        private bool TryGetTimer(object keyID, out ProTimer timer, out Dictionary<object, ProTimer> timerDictionary)
        {
            // 尝试从普通计时器字典中获取计时器对象
            if (_normalTimerDictionary.TryGetValue(keyID, out timer))
            {
                // 如果找到，将字典设置为普通计时器字典
                timerDictionary = _normalTimerDictionary;
                return true; // 返回true表示找到计时器
            }

            // 尝试从真实时间计时器字典中获取计时器对象
            if (_realTimerDictionary.TryGetValue(keyID, out timer))
            {
                // 如果找到，将字典设置为真实时间计时器字典
                timerDictionary = _realTimerDictionary;
                return true; // 返回true表示找到计时器
            }

            // 如果在两个字典中都未找到计时器
            timerDictionary = null; // 将输出参数设置为null
            return false; // 返回false表示未找到计时器
        }
    }
}