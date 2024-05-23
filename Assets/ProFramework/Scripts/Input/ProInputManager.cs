using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProFramework
{
    public class ProInputManager : ProSingletonInSystem<ProInputManager>
    {
        private Dictionary<System.Enum, ProInputInfo> _inputDictionary = new Dictionary<System.Enum, ProInputInfo>();

        private ProInputInfo _nowProInputInfo;
        private bool _isInputDetectionEnabled;
        private bool _isChangeKeyState = false;
        private KeyCode _oldKey = KeyCode.None;

        private ProInputManager()
        {
            ProMonoManager.Instance.AddUpdateListener(InputUpdate);
        }

        /// <summary>
        /// 启用或禁用输入检测
        /// </summary>
        /// <param name="isInputDetectionEnabled">是否启用输入检测</param>
        public void ChangeInputDetectionEnabled(bool isInputDetectionEnabled)
        {
            _isInputDetectionEnabled = isInputDetectionEnabled;
        }

        /// <summary>
        /// 添加输入信息
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="key">键码</param>
        /// <param name="inputType">输入类型</param>
        public void AddInputInfo(System.Enum eventType, KeyCode key, EProInputType inputType)
        {
            if (!_inputDictionary.ContainsKey(eventType))
            {
                _inputDictionary.Add(eventType, new ProInputInfo(inputType, key));
            }
            else
            {
                ProLog.LogWarning("该事件已经存在按键映射！");
            }
        }

        /// <summary>
        /// 修改输入信息
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="key">键码</param>
        /// <param name="inputType">输入类型</param>
        public void ChangeInputInfo(System.Enum eventType, KeyCode key = KeyCode.None,
            EProInputType inputType = EProInputType.None)
        {
            if (!_inputDictionary.ContainsKey(eventType))
            {
                ProLog.LogWarning("不存在该事件的按键映射！请先添加该事件的按键映射！");
                return;
            }

            if (key != KeyCode.None)
            {
                _inputDictionary[eventType].Key = key;
                ProLog.LogDebug($"{eventType}修改输入映射为{key}");
            }

            if (inputType != EProInputType.None)
            {
                _inputDictionary[eventType].InputType = inputType;
                ProLog.LogDebug($"{eventType}修改输入类型为{inputType}");
            }
        }

        /// <summary>
        /// 移除输入信息
        /// </summary>
        /// <param name="eventType">事件类型</param>
        public void RemoveInputInfo(System.Enum eventType)
        {
            if (_inputDictionary.ContainsKey(eventType))
            {
                _inputDictionary.Remove(eventType);
            }
        }

        /// <summary>
        /// 开始修改按键输入
        /// </summary>
        /// <param name="oldKey">旧的键码</param>
        public void BeginChangeKeyInput(KeyCode oldKey)
        {
            if (oldKey == KeyCode.None)
            {
                ProLog.LogWarning("不可以从无选择按键改键");
                return;
            }

            ProMonoManager.Instance.StartCoroutine(BeginChangeKeyInputCoroutine(oldKey));
        }

        private IEnumerator BeginChangeKeyInputCoroutine(KeyCode oldKey)
        {
            yield return null; // 等一帧
            _isChangeKeyState = true;
            _oldKey = oldKey;
        }

        private void InputUpdate()
        {
            if (_isChangeKeyState)
            {
                if (Input.anyKeyDown)
                {
                    foreach (KeyCode inputKey in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(inputKey) && inputKey != KeyCode.None && _oldKey != KeyCode.None)
                        {
                            foreach (var kvp in _inputDictionary)
                            {
                                if (kvp.Value.Key == _oldKey)
                                {
                                    ChangeInputInfo(kvp.Key, inputKey);
                                }
                            }
                        }
                    }

                    _isChangeKeyState = false;
                    _oldKey = KeyCode.None;
                    return;
                }
            }

            if (!_isInputDetectionEnabled)
                return;

            foreach (var eventType in _inputDictionary.Keys)
            {
                _nowProInputInfo = _inputDictionary[eventType];

                switch (_nowProInputInfo.InputType)
                {
                    case EProInputType.Down:
                        if (Input.GetKeyDown(_nowProInputInfo.Key))
                            ProEventManager.Instance.EventTrigger(eventType);
                        break;
                    case EProInputType.Up:
                        if (Input.GetKeyUp(_nowProInputInfo.Key))
                            ProEventManager.Instance.EventTrigger(eventType);
                        break;
                    case EProInputType.Hold:
                        if (Input.GetKey(_nowProInputInfo.Key))
                            ProEventManager.Instance.EventTrigger(eventType);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}