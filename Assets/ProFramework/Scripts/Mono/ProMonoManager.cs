using UnityEngine;
using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// 公共Mono模块管理器，负责管理Unity的生命周期函数
    /// </summary>
    public class ProMonoManager : ProSingletonInMonoAuto<ProMonoManager>
    {
        /// <summary>
        /// Update帧更新事件
        /// </summary>
        private event UnityAction UpdateEvent;

        /// <summary>
        /// FixedUpdate帧更新事件
        /// </summary>
        private event UnityAction FixedUpdateEvent;

        /// <summary>
        /// LateUpdate帧更新事件
        /// </summary>
        private event UnityAction LateUpdateEvent;

        /// <summary>
        /// 添加Update帧更新监听函数
        /// </summary>
        /// <param name="updateAction">Update帧更新监听函数</param>
        public void AddUpdateListener(UnityAction updateAction)
        {
            UpdateEvent += updateAction;
        }

        /// <summary>
        /// 移除Update帧更新监听函数
        /// </summary>
        /// <param name="updateAction">Update帧更新监听函数</param>
        public void RemoveUpdateListener(UnityAction updateAction)
        {
            UpdateEvent -= updateAction;
        }

        /// <summary>
        /// 添加FixedUpdate帧更新监听函数
        /// </summary>
        /// <param name="fixedUpdateAction">FixedUpdate帧更新监听函数</param>
        public void AddFixedUpdateListener(UnityAction fixedUpdateAction)
        {
            FixedUpdateEvent += fixedUpdateAction;
        }

        /// <summary>
        /// 移除FixedUpdate帧更新监听函数
        /// </summary>
        /// <param name="fixedUpdateAction">FixedUpdate帧更新监听函数</param>
        public void RemoveFixedUpdateListener(UnityAction fixedUpdateAction)
        {
            FixedUpdateEvent -= fixedUpdateAction;
        }

        /// <summary>
        /// 添加LateUpdate帧更新监听函数
        /// </summary>
        /// <param name="lateUpdateAction">LateUpdate帧更新监听函数</param>
        public void AddLateUpdateListener(UnityAction lateUpdateAction)
        {
            LateUpdateEvent += lateUpdateAction;
        }

        /// <summary>
        /// 移除LateUpdate帧更新监听函数
        /// </summary>
        /// <param name="lateUpdateAction">LateUpdate帧更新监听函数</param>
        public void RemoveLateUpdateListener(UnityAction lateUpdateAction)
        {
            LateUpdateEvent -= lateUpdateAction;
        }

        /// <summary>
        /// Update帧更新方法
        /// </summary>
        private void Update()
        {
            UpdateEvent?.Invoke();
        }

        /// <summary>
        /// FixedUpdate帧更新方法
        /// </summary>
        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }

        /// <summary>
        /// LateUpdate帧更新方法
        /// </summary>
        private void LateUpdate()
        {
            LateUpdateEvent?.Invoke();
        }
    }
}