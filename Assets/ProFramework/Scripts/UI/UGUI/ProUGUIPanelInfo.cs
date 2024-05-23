using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// 用于存储面板信息 和加载完成的回调函数的
    /// </summary>
    /// <typeparam name="T">面板的类型</typeparam>
    internal class ProUGUIPanelInfo<T> : IProUGUIPanelInfo where T : ProUGUIPanel
    {
        public T panel;
        public UnityAction<T> callBack;
        public bool isHide;

        public ProUGUIPanelInfo(UnityAction<T> callBack)
        {
            this.callBack += callBack;
        }
    }
}