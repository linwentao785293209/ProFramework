using System;
using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class InputTest : MonoBehaviour
    {
        void Start()
        {
            // 为技能添加监听
            RegisterSkillListeners();

            // 启用输入检测
            ProInputManager.Instance.ChangeInputDetectionEnabled(true);

            // 关联QWER键到技能
            RegisterSkillInputs();
        }

        /// <summary>
        /// 注册技能事件监听器
        /// </summary>
        void RegisterSkillListeners()
        {
            ProEventManager.Instance.AddEventListener(ESkillTypeTest.Skill1, () => { ProLog.LogDebug("1技能释放"); });

            ProEventManager.Instance.AddEventListener(ESkillTypeTest.Skill2, () => { ProLog.LogDebug("2技能释放"); });

            ProEventManager.Instance.AddEventListener(ESkillTypeTest.Skill3, () => { ProLog.LogDebug("3技能释放"); });

            ProEventManager.Instance.AddEventListener(ESkillTypeTest.SkillUltimate,
                () => { ProLog.LogDebug("终极技能释放"); });
        }

        /// <summary>
        /// 注册技能输入映射
        /// </summary>
        void RegisterSkillInputs()
        {
            RegisterSkillInput(ESkillTypeTest.Skill1, KeyCode.Q);
            RegisterSkillInput(ESkillTypeTest.Skill2, KeyCode.W);
            RegisterSkillInput(ESkillTypeTest.Skill3, KeyCode.E);
            RegisterSkillInput(ESkillTypeTest.SkillUltimate, KeyCode.R);
        }

        /// <summary>
        /// 注册单个技能的输入映射
        /// </summary>
        /// <param name="skillType">技能类型</param>
        /// <param name="key">键码</param>
        void RegisterSkillInput(ESkillTypeTest skillType, KeyCode key)
        {
            ProInputManager.Instance.AddInputInfo(skillType, key, EProInputType.Down);
        }

        private void Update()
        {
            // 用于更改按键绑定的测试
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ProInputManager.Instance.BeginChangeKeyInput(KeyCode.Q);
            }
        }
    }
}