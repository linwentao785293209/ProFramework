using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// 计时器对象 里面存储了计时器的相关数据
    /// </summary>
    internal class ProTimer : IProSystemObject
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public object keyID;

        /// <summary>
        /// 计时结束后的委托回调
        /// </summary>
        public UnityAction overCallBack;

        /// <summary>
        /// 间隔一定时间去执行的委托回调
        /// </summary>
        public UnityAction callBack;

        /// <summary>
        /// 表示计时器总的计时时间 毫秒：1s = 1000ms
        /// </summary>
        public int nowAllTime;

        /// <summary>
        /// 记录一开始计时时的总时间 用于时间重置
        /// </summary>
        public int maxAllTime;

        /// <summary>
        /// 间隔执行回调的时间 毫秒 毫秒：1s = 1000ms
        /// </summary>
        public int nowIntervalTime;

        /// <summary>
        /// 记录一开始的间隔时间
        /// </summary>
        public int maxIntervalTime;

        /// <summary>
        /// 是否在进行计时
        /// </summary>
        public bool isRunning;

        /// <summary>
        /// 初始化计时器数据
        /// </summary>
        /// <param name="keyID">唯一ID</param>
        /// <param name="allTime">总的时间</param>
        /// <param name="overCallBack">总时间计时结束后的回调</param>
        /// <param name="intervalTime">间隔执行的时间</param>
        /// <param name="callBack">间隔执行时间结束后的回调</param>
        public void Init(object keyID, int allTime, UnityAction overCallBack, int intervalTime = 0,
            UnityAction callBack = null)
        {
            if (allTime < 0)
            {
                throw new ArgumentException("倒计时时间必须是非负值");
            }

            if (intervalTime < 0)
            {
                throw new ArgumentException("间隔时间必须是非负值");
            }

            this.keyID = keyID;
            this.maxAllTime = this.nowAllTime = allTime;
            this.overCallBack = overCallBack;
            this.maxIntervalTime = this.nowIntervalTime = intervalTime;
            this.callBack = callBack;
            this.isRunning = true;
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void ResetTimer()
        {
            this.nowAllTime = this.maxAllTime;
            this.nowIntervalTime = this.maxIntervalTime;
        }

        /// <summary>
        /// 缓存池回收时  清除相关引用数据
        /// </summary>
        public void ResetInfo()
        {
            overCallBack = null;
            callBack = null;
        }
    }
}