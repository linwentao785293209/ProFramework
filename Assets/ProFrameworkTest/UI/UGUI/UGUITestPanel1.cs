using ProFramework;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace ProFrameworkTest
{
    public class UGUITestPanel1 : ProUGUIPanel
    {
        protected override void OnButtonClick(string btnName)
        {
            base.OnButtonClick(btnName);

            switch (btnName)
            {
                case "btnBegin":
                    ProLog.LogDebug("btnBegin被点击");
                    break;

                case "btnSetting":
                    ProLog.LogDebug("btnSetting被点击");
                    break;

                case "btnQuit":
                    ProLog.LogDebug("btnQuit被点击");
                    break;
            }
        }

        protected override void OnStart()
        {
            // 获取开始按钮
            Button beginButton = GetUIControl<Button>("btnBegin");

            // 添加鼠标进入事件
            ProUGUIManager.AddEventTriggerListener(beginButton, EventTriggerType.PointerEnter,
                OnBeginButtonPointerEnter);

            // 添加鼠标退出事件
            ProUGUIManager.AddEventTriggerListener(beginButton, EventTriggerType.PointerExit, OnBeginButtonPointerExit);
        }

        internal override void OnShow()
        {
            ProLog.LogDebug("UGUITestPanel1在显示");
        }


        internal override void OnHide()
        {
            ProLog.LogDebug("UGUITestPanel1被隐藏");
        }

        protected override void OnDispose()
        {
            // 获取开始按钮
            Button beginButton = GetUIControl<Button>("btnBegin");

            // 移除鼠标进入事件
            ProUGUIManager.RemoveEventTriggerListener(beginButton, EventTriggerType.PointerEnter,
                OnBeginButtonPointerEnter);

            // 移除鼠标退出事件
            ProUGUIManager.RemoveEventTriggerListener(beginButton, EventTriggerType.PointerExit,
                OnBeginButtonPointerExit);
        }

        // 鼠标进入事件处理函数
        private void OnBeginButtonPointerEnter(BaseEventData eventData)
        {
            ProLog.LogDebug("鼠标进入开始按钮");
        }

        // 鼠标退出事件处理函数
        private void OnBeginButtonPointerExit(BaseEventData eventData)
        {
            ProLog.LogDebug("鼠标退出开始按钮");
        }
    }
}