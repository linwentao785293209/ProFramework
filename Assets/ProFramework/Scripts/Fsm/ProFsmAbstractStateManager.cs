using System.Collections.Generic;
using UnityEngine;

namespace ProFramework
{
    // 抽象状态机管理器
    public abstract class ProFsmAbstractStateManager<TStateEnum, TTransitionEnum>
        where TStateEnum : System.Enum
        where TTransitionEnum : System.Enum
    {
        protected Dictionary<TStateEnum, ProFsmAbstractState<TStateEnum, TTransitionEnum>> stateDictionary =
            new Dictionary<TStateEnum, ProFsmAbstractState<TStateEnum, TTransitionEnum>>(); // 状态字典

        protected ProFsmAbstractState<TStateEnum, TTransitionEnum> currentState; // 当前状态

        public ProFsmAbstractState<TStateEnum, TTransitionEnum> CurrentState => currentState; // 获取当前状态


        // 启动状态机
        public void Start(TStateEnum state)
        {
            if (EqualityComparer<TStateEnum>.Default.Equals(state, default))
            {
                ProLog.LogError("状态ID为空");
                return;
            }

            if (!stateDictionary.ContainsKey(state))
            {
                ProLog.LogError($"状态 {state} 不存在");
                return;
            }

            currentState = stateDictionary[state]; // 设置初始状态
            currentState.OnEnter(); // 执行进入状态的操作
        }

        // 更新状态机
        public virtual void Update()
        {
            currentState.OnUpdate(); // 执行当前状态的更新操作
            currentState.CheckTransition();
        }

        // 添加状态
        public void AddState(ProFsmAbstractState<TStateEnum, TTransitionEnum> state)
        {
            if (state == null)
            {
                ProLog.LogError("传入状态为空");
                return;
            }

            if (EqualityComparer<TStateEnum>.Default.Equals(state.StateEnum, default))
            {
                ProLog.LogError("传入状态的状态枚举为空");
                return;
            }

            if (stateDictionary.ContainsKey(state.StateEnum))
            {
                ProLog.LogError("状态已存在");
                return;
            }

            stateDictionary.Add(state.StateEnum, state);
        }

        // 移除状态
        public void RemoveState(ProFsmAbstractState<TStateEnum, TTransitionEnum> state)
        {
            if (state == null)
            {
                ProLog.LogError("传入状态为空");
                return;
            }

            if (EqualityComparer<TStateEnum>.Default.Equals(state.StateEnum, default))
            {
                ProLog.LogError("传入状态的状态枚举为空");
                return;
            }

            if (!stateDictionary.ContainsKey(state.StateEnum))
            {
                ProLog.LogError("状态不存在");
                return;
            }

            stateDictionary.Remove(state.StateEnum);
        }

        // 状态转换
        public void ChangeState(TTransitionEnum transition)
        {
            if (EqualityComparer<TTransitionEnum>.Default.Equals(transition, default))
            {
                ProLog.LogError("转换条件为空");
                return;
            }

            TStateEnum nextState = currentState.GetState(transition);

            if (EqualityComparer<TStateEnum>.Default.Equals(nextState, default))
            {
                return; // 如果转换条件对应的状态不存在，直接返回
            }

            if (!stateDictionary.ContainsKey(nextState))
            {
                ProLog.LogError($"状态 {nextState} 不存在");
                return;
            }

            currentState.OnExit(); // 执行当前状态的退出操作
            currentState = stateDictionary[nextState]; // 设置新状态
            currentState.OnEnter(); // 执行新状态的进入操作
        }
    }
}