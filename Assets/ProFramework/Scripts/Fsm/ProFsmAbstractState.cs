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

        protected Dictionary<TTransitionEnum, TStateEnum> transitionToStateDictionary = new Dictionary<TTransitionEnum, TStateEnum>(); // 状态转换字典

        protected ProFsmAbstractStateManager<TStateEnum, TTransitionEnum> stateManager;
        
        // 构造函数，初始化状态ID
        protected ProFsmAbstractState(TStateEnum stateEnum,ProFsmAbstractStateManager<TStateEnum, TTransitionEnum> stateManager)
        {
            this.stateEnum = stateEnum;
            this.stateManager = stateManager;
        }

        // 添加状态转换 
        public void AddTransition(TTransitionEnum transition, TStateEnum state)
        {
            if (EqualityComparer<TTransitionEnum>.Default.Equals(transition, default) || EqualityComparer<TStateEnum>.Default.Equals(state, default))
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
        public abstract void OnEnter();

        // 离开状态时的操作
        public abstract void OnExit();

        // 更新状态时的操作
        public abstract void OnUpdate();
        public abstract void CheckTransition();
    }
}