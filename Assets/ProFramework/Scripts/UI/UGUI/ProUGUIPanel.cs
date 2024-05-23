using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProFramework
{
    public abstract class ProUGUIPanel : MonoBehaviour
    {
        protected Dictionary<string, List<UIBehaviour>> uiControlDictionary =
            new Dictionary<string, List<UIBehaviour>>();

        private static readonly List<string> DefaultControlNameList = new List<string>()
        {
            "Image",
            "Text (TMP)",
            "RawImage",
            "Background",
            "Checkmark",
            "Label",
            "Text (Legacy)",
            "Arrow",
            "Placeholder",
            "Fill",
            "Handle",
            "Viewport",
            "Scrollbar Horizontal",
            "Scrollbar Vertical",
            "Slider",
            "Toggle"
        };

        protected virtual void Awake()
        {
            FindUIControl<Button>();
            FindUIControl<Toggle>();
            FindUIControl<Slider>();
            FindUIControl<InputField>();
            FindUIControl<ScrollRect>();
            FindUIControl<Dropdown>();
            FindUIControl<Text>();
            FindUIControl<TextMeshPro>();
            FindUIControl<Image>();
        }

        protected virtual void Start()
        {
            ProLog.LogDebug($"{this.gameObject.name}被创建！");
            OnStart();
        }

        protected virtual void OnDestroy()
        {
            ProLog.LogDebug($"{this.gameObject.name}被销毁！");
            OnDispose();
        }

        protected abstract void OnStart();
        
        internal abstract void OnShow();

        internal abstract void OnHide();

        protected abstract void OnDispose();
        
        private void FindUIControl<T>() where T : UIBehaviour
        {
            T[] controls = this.GetComponentsInChildren<T>(true);

            foreach (var control in controls)
            {
                string gameObjectName = control.gameObject.name;

                if (DefaultControlNameList.Contains(gameObjectName))
                {
                    continue;
                }

                if (!uiControlDictionary.ContainsKey(gameObjectName))
                {
                    uiControlDictionary.Add(gameObjectName, new List<UIBehaviour>());
                }

                uiControlDictionary[gameObjectName].Add(control);

                if (control is Button)
                {
                    (control as Button)?.onClick.AddListener(() => { OnButtonClick(gameObjectName); });
                }
                else if (control is Slider)
                {
                    (control as Slider)?.onValueChanged.AddListener((value) =>
                    {
                        OnSliderValueChange(gameObjectName, value);
                    });
                }
                else if (control is Toggle)
                {
                    (control as Toggle)?.onValueChanged.AddListener((value) =>
                    {
                        OnToggleValueChange(gameObjectName, value);
                    });
                }
            }
        }

        public T GetUIControl<T>(string gameObjectName) where T : UIBehaviour
        {
            if (uiControlDictionary.ContainsKey(gameObjectName))
            {
                List<UIBehaviour> controls = uiControlDictionary[gameObjectName];
                List<T> typedControls = new List<T>();

                foreach (var control in controls)
                {
                    if (control is T)
                    {
                        typedControls.Add((T)control);
                    }
                }

                if (typedControls.Count == 0)
                {
                    ProLog.LogError($"没有找到名称为{gameObjectName}的类型为{typeof(T)}的组件");
                    return null;
                }

                if (typedControls.Count >= 2)
                {
                    ProLog.LogWarning($"对象名称为{gameObjectName}的类型为{typeof(T)}的组件有多个，只返回第一个，不建议一个对象上挂多个相同组件，请检查！");
                }

                return typedControls[0];
            }
            else
            {
                ProLog.LogError($"没有找到带有该名称的UI控件{gameObjectName}");
                return null;
            }
        }


        // 动态添加控件
        protected void AddUIControl<T>(string gameObjectName, T uiControl) where T : UIBehaviour
        {
            if (uiControlDictionary.ContainsKey(gameObjectName))
            {
                uiControlDictionary[gameObjectName].Add(uiControl);

                // 检查是否已存在相同类型的组件，并发出警告
                foreach (var control in uiControlDictionary[gameObjectName])
                {
                    if (control.GetType() == uiControl.GetType())
                    {
                        ProLog.LogWarning($"对象 {gameObjectName} 中已存在类型为 {typeof(T)} 的组件，添加重复组件！");
                        break;
                    }
                }
            }
            else
            {
                uiControlDictionary.Add(gameObjectName, new List<UIBehaviour>() { uiControl });
            }
        }


        // 动态移除控件
        protected void RemoveUIControl<T>(string gameObjectName, T uiControl) where T : UIBehaviour
        {
            if (uiControlDictionary.ContainsKey(gameObjectName))
            {
                if (uiControlDictionary[gameObjectName].Contains(uiControl))
                {
                    uiControlDictionary[gameObjectName].Remove(uiControl);

                    // 如果列表为空，从字典中移除
                    if (uiControlDictionary[gameObjectName].Count == 0)
                    {
                        uiControlDictionary.Remove(gameObjectName);
                    }
                }
                else
                {
                    ProLog.LogError($"尝试移除的控件 {uiControl} 不存在于 {gameObjectName}");
                }
            }
            else
            {
                ProLog.LogError($"控件列表中不存在名为 {gameObjectName} 的控件");
            }
        }


        protected virtual void OnButtonClick(string btnName)
        {
        }

        protected virtual void OnSliderValueChange(string sliderName, float value)
        {
        }

        protected virtual void OnToggleValueChange(string toggleName, bool value)
        {
        }
    }
}