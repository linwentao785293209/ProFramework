using System.Collections.Generic;
using UnityEngine;

namespace ProFramework
{
    // 抽象状态基类
    public abstract class ProFsmAbstractState<TStateEnum, TTransitionEnum>
        where TStateEnum : System.Enum
        where TTransitionEnum : System.Enum
    {
        protected TStateEnum stateEnum; // 状态ID
        public TStateEnum StateEnum => stateEnum; // 获取状态ID

        protected Dictionary<TTransitionEnum, TStateEnum> transitionToStateDictionary =
            new Dictionary<TTransitionEnum, TStateEnum>(); // 状态转换字典

        protected ProFsmAbstractStateManager<TStateEnum, TTransitionEnum> stateManager;

        private float stateStartTime; // 状态开始时间
        protected float stateDurationTime; // 状态持续时间

        // 构造函数，初始化状态ID
        protected ProFsmAbstractState(TStateEnum stateEnum,
            ProFsmAbstractStateManager<TStateEnum, TTransitionEnum> stateManager)
        {
            this.stateEnum = stateEnum;
            this.stateManager = stateManager;
            stateDurationTime = 0f; // 初始化状态持续时间
        }

        // 添加状态转换 
        public void AddTransition(TTransitionEnum transition, TStateEnum state)
        {
            if (EqualityComparer<TTransitionEnum>.Default.Equals(transition, default) ||
                EqualityComparer<TStateEnum>.Default.Equals(state, default))
            {
                ProLog.LogError("转换条件或状态为空");
                return;
            }

            if (transitionToStateDictionary.ContainsKey(transition))
            {
                ProLog.LogError($"转换条件 {transition} 已存在");
                return;
            }

            transitionToStateDictionary.Add(transition, state); // 添加转换条件和对应状态
        }

        // 移除状态转换
        public void RemoveTransition(TTransitionEnum transition)
        {
            if (!transitionToStateDictionary.ContainsKey(transition))
            {
                ProLog.LogError($"转换条件 {transition} 不存在");
                return;
            }

            transitionToStateDictionary.Remove(transition); // 移除转换条件
        }

        // 根据转换条件获取目标状态
        public TStateEnum GetState(TTransitionEnum transition)
        {
            if (transitionToStateDictionary.ContainsKey(transition))
            {
                return transitionToStateDictionary[transition]; // 获取目标状态
            }

            ProLog.LogError($"对于当前状态{stateEnum},转换条件 {transition} 不存在,无法转换！");
            return default; // 如果转换条件不存在，返回默认状态
        }

        // 进入状态时的操作
        public virtual void OnEnter()
        {
            ResetStateDuration(); // 重置状态持续时间
        }

        // 离开状态时的操作
        public virtual void OnExit()
        {
            // 在这里可以添加离开状态时的逻辑
        }

        // 更新状态时的操作
        public virtual void OnUpdate()
        {
            UpdateStateDuration(Time.deltaTime); // 更新状态持续时间
        }

        // 检查状态转换的操作
        public abstract void CheckTransition();

        // 更新状态持续时间
        protected void UpdateStateDuration(float deltaTime)
        {
            stateDurationTime += deltaTime;
        }

        // 重置状态持续时间
        protected void ResetStateDuration()
        {
            stateStartTime = Time.time;
            stateDurationTime = 0f;
        }
    }
}
