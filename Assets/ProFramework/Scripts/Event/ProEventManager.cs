using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace ProFramework
{
    /// <summary>
    /// 事件中心模块 
    /// </summary>
    public class ProEventManager : ProSingletonInSystem<ProEventManager>
    {
        //用于记录对应事件 关联的 对应的逻辑
        private Dictionary<System.Enum, IProEventInfo> eventInfoDictionary =
            new Dictionary<System.Enum, IProEventInfo>();


        private ProEventManager()
        {
        }

        /// <summary>
        /// 触发事件 
        /// </summary>
        /// <param name="eventType">事件名字</param>
        public void EventTrigger<T>(System.Enum eventType, T info)
        {
            //存在关心我的人 才通知别人去处理逻辑
            if (eventInfoDictionary.ContainsKey(eventType))
            {
                //去执行对应的逻辑
                (eventInfoDictionary[eventType] as ProEventInfo<T>).actions?.Invoke(info);
            }
            else
            {
                ProLog.LogWarning($"还不存在{eventType}这个事件");
            }
        }

        /// <summary>
        /// 触发事件 无参数
        /// </summary>
        /// <param name="eventType"></param>
        public void EventTrigger(System.Enum eventType)
        {
            //存在关心我的人 才通知别人去处理逻辑
            if (eventInfoDictionary.ContainsKey(eventType))
            {
                //去执行对应的逻辑
                (eventInfoDictionary[eventType] as ProEventInfo).actions?.Invoke();
            }
            else
            {
                ProLog.LogWarning($"还不存在{eventType}这个事件");
            }
        }


        /// <summary>
        /// 添加事件监听者
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void AddEventListener<T>(System.Enum eventType, UnityAction<T> action)
        {
            //如果已经存在关心事件的委托记录 直接添加即可
            if (eventInfoDictionary.ContainsKey(eventType))
            {
                (eventInfoDictionary[eventType] as ProEventInfo<T>).actions += action;
            }
            else
            {
                eventInfoDictionary.Add(eventType, new ProEventInfo<T>(action));
            }
        }

        public void AddEventListener(System.Enum eventType, UnityAction action)
        {
            //如果已经存在关心事件的委托记录 直接添加即可
            if (eventInfoDictionary.ContainsKey(eventType))
            {
                (eventInfoDictionary[eventType] as ProEventInfo).actions += action;
            }
            else
            {
                eventInfoDictionary.Add(eventType, new ProEventInfo(action));
            }
        }

        /// <summary>
        /// 移除事件监听者
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void RemoveEventListener<T>(System.Enum eventType, UnityAction<T> action)
        {
            if (eventInfoDictionary.ContainsKey(eventType))
                (eventInfoDictionary[eventType] as ProEventInfo<T>).actions -= action;
        }

        public void RemoveEventListener(System.Enum eventType, UnityAction action)
        {
            if (eventInfoDictionary.ContainsKey(eventType))
                (eventInfoDictionary[eventType] as ProEventInfo).actions -= action;
        }

        /// <summary>
        /// 清空所有事件的监听
        /// </summary>
        public void Clear()
        {
            eventInfoDictionary.Clear();
        }

        /// <summary>
        /// 清除指定某一个事件的所有监听
        /// </summary>
        /// <param name="eventName"></param>
        public void Clear(System.Enum eventName)
        {
            if (eventInfoDictionary.ContainsKey(eventName))
                eventInfoDictionary.Remove(eventName);
        }
    }
}