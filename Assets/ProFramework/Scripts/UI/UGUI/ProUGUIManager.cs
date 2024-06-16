using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace ProFramework
{
    /// <summary>
    /// 管理所有UI面板的管理器
    /// 注意：面板预设体名要和面板类名一致！！！！！
    /// </summary>
    public class ProUGUIManager : ProSingletonInSystem<ProUGUIManager>
    {
        private Camera _camera;
        private Canvas _canvas;
        private EventSystem _eventSystem;

        //层级父对象
        private Transform bottomLayer;
        private Transform middleLayer;
        private Transform topLayer;
        private Transform systemLayer;

        /// <summary>
        /// 用于存储所有的面板对象
        /// </summary>
        private Dictionary<string, IProUGUIPanelInfo>
            _panelInfoDictionary = new Dictionary<string, IProUGUIPanelInfo>();

        private ProUGUIManager()
        {
            ProResourceManager.Instance.LoadResource<GameObject>(ProConst.Ui, "UGUICamera",
                (cameraGameObject =>
                {
                    _camera = GameObject.Instantiate(cameraGameObject).GetComponent<Camera>();

                    //ui摄像机过场景不移除 专门用来渲染UI面板
                    GameObject.DontDestroyOnLoad(_camera.gameObject);


                    ProResourceManager.Instance.LoadResource<GameObject>(ProConst.Ui, "UGUICanvas",
                        (canvasGameObject) =>
                        {
                            _canvas = GameObject.Instantiate(canvasGameObject).GetComponent<Canvas>();

                            //设置使用的UI摄像机
                            _canvas.worldCamera = _camera;

                            //过场景不移除
                            GameObject.DontDestroyOnLoad(_canvas.gameObject);

                            //找到层级父对象
                            bottomLayer = _canvas.transform.Find("Bottom");
                            middleLayer = _canvas.transform.Find("Middle");
                            topLayer = _canvas.transform.Find("Top");
                            systemLayer = _canvas.transform.Find("System");

                            ProResourceManager.Instance.LoadResource<GameObject>(ProConst.Ui, "UGUIEventSystem",
                                (eventSystemGameObject) =>
                                {
                                    _eventSystem = GameObject.Instantiate(eventSystemGameObject)
                                        .GetComponent<EventSystem>();

                                    GameObject.DontDestroyOnLoad(_eventSystem.gameObject);
                                });
                        }, true);
                }), true);
        }

        /// <summary>
        /// 获取对应层级的父对象
        /// </summary>
        /// <param name="layer">层级枚举值</param>
        /// <returns></returns>
        public Transform GetLayerTransform(EProUGUILayer layer)
        {
            switch (layer)
            {
                case EProUGUILayer.Bottom:
                    return bottomLayer;
                case EProUGUILayer.Middle:
                    return middleLayer;
                case EProUGUILayer.Top:
                    return topLayer;
                case EProUGUILayer.System:
                    return systemLayer;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 显示面板
        /// </summary>
        /// <typeparam name="T">面板的类型</typeparam>
        /// <param name="layer">面板显示的层级</param>
        /// <param name="callBack">由于可能是异步加载 因此通过委托回调的形式 将加载完成的面板传递出去进行使用</param>
        /// <param name="isSync">是否采用同步加载 默认为false</param>
        public void ShowPanel<T>(EProUGUILayer layer = EProUGUILayer.Bottom,string assetBundleName = ProConst.Ui, UnityAction<T> callBack = null,
            bool isSync = false) where T : ProUGUIPanel
        {
            //获取面板名 预设体名必须和面板类名一致 
            string panelName = typeof(T).Name;

            //存在面板
            if (_panelInfoDictionary.ContainsKey(panelName))
            {
                //取出字典中已经占好位置的数据
                ProUGUIPanelInfo<T> panelInfo = _panelInfoDictionary[panelName] as ProUGUIPanelInfo<T>;

                //正在异步加载中
                if (panelInfo.panel == null)
                {
                    //如果之前显示了又隐藏 现在又想显示 那么直接设为false
                    panelInfo.isHide = false;

                    //如果正在异步加载 应该等待它加载完毕 只需要记录回调函数 加载完后去调用即可
                    if (callBack != null)
                        panelInfo.callBack += callBack;
                }
                else //已经加载结束
                {
                    //如果是失活状态 直接激活面板 就可以显示了
                    if (!panelInfo.panel.gameObject.activeSelf)
                        panelInfo.panel.gameObject.SetActive(true);

                    //如果要显示面板 会执行一次面板的默认显示逻辑
                    panelInfo.panel.OnShow();

                    //如果存在回调 直接返回出去即可
                    callBack?.Invoke(panelInfo.panel);
                }

                return;
            }

            //不存在面板 先存入字典当中 占个位置 之后如果又显示 我才能得到字典中的信息进行判断
            _panelInfoDictionary.Add(panelName, new ProUGUIPanelInfo<T>(callBack));

            //不存在面板 加载面板
            ProAssetManager.Instance.LoadResource<GameObject>(assetBundleName, panelName, (res) =>
            {
                //取出字典中已经占好位置的数据
                ProUGUIPanelInfo<T> panelInfo = _panelInfoDictionary[panelName] as ProUGUIPanelInfo<T>;


                //表示异步加载结束前 就想要隐藏该面板了 
                if (panelInfo != null && panelInfo.isHide)
                {
                    _panelInfoDictionary.Remove(panelName);
                    return;
                }

                //层级的处理
                Transform layerTransform = GetLayerTransform(layer);

                //避免没有按指定规则传递层级参数 避免为空
                if (layerTransform == null)
                    layerTransform = bottomLayer;

                //将面板预设体创建到对应父对象下 并且保持原本的缩放大小
                GameObject panelGameObject = GameObject.Instantiate(res, layerTransform, false);

                //获取对应UI组件返回出去
                T panel = panelGameObject.GetComponent<T>();

                //显示面板时执行的默认方法
                panel.OnShow();

                //传出去使用
                panelInfo.callBack?.Invoke(panel);

                //回调执行完 将其清空 避免内存泄漏
                panelInfo.callBack = null;

                //存储panel
                panelInfo.panel = panel;
            }, isSync);
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <typeparam name="T">面板类型</typeparam>
        public void HidePanel<T>(bool isDestory = false) where T : ProUGUIPanel
        {
            string panelName = typeof(T).Name;

            if (_panelInfoDictionary.ContainsKey(panelName))
            {
                //取出字典中已经占好位置的数据
                ProUGUIPanelInfo<T> panelInfo = _panelInfoDictionary[panelName] as ProUGUIPanelInfo<T>;

                //但是正在加载中
                if (panelInfo != null && panelInfo.panel == null)
                {
                    //修改隐藏表示 表示 这个面板即将要隐藏
                    panelInfo.isHide = true;

                    ProLog.LogDebug($"{panelName}面板未加载完成，无需隐藏。");

                    //既然要隐藏了 回调函数都不会调用了 直接置空
                    panelInfo.callBack = null;
                }
                else //已经加载结束
                {
                    //执行默认的隐藏面板想要做的事情
                    if (panelInfo != null)
                    {
                        panelInfo.panel.OnHide();

                        //如果要销毁  就直接将面板销毁从字典中移除记录
                        if (isDestory)
                        {
                            //销毁面板
                            GameObject.Destroy(panelInfo.panel.gameObject);

                            //从容器中移除
                            _panelInfoDictionary.Remove(panelName);
                        }
                        //如果不销毁 那么就只是失活 下次再显示的时候 直接复用即可
                        else
                        {
                            panelInfo.panel.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取面板
        /// </summary>
        /// <typeparam name="T">面板的类型</typeparam>
        public void GetPanel<T>(UnityAction<T> callBack) where T : ProUGUIPanel
        {
            string panelName = typeof(T).Name;

            if (_panelInfoDictionary.ContainsKey(panelName))
            {
                //取出字典中已经占好位置的数据
                ProUGUIPanelInfo<T> panelInfo = _panelInfoDictionary[panelName] as ProUGUIPanelInfo<T>;

                //正在加载中
                if (panelInfo.panel == null)
                {
                    //加载中 应该等待加载结束 再通过回调传递给外部去使用
                    panelInfo.callBack += callBack;
                }
                else
                {
                    if (panelInfo.isHide)
                    {
                        ProLog.LogDebug($"存在{panelName}面板,但是面板被隐藏，请检查是否需要显示！");
                    }

                    callBack?.Invoke(panelInfo.panel);
                }
            }
            else
            {
                ProLog.LogWarning($"不存在{panelName}面板,无法获取！");
            }
        }


        /// <summary>
        /// 为控件添加自定义事件
        /// </summary>
        /// <param name="control">对应的控件</param>
        /// <param name="type">事件的类型</param>
        /// <param name="callBack">响应的函数</param>
        public static void AddEventTriggerListener(UIBehaviour control, EventTriggerType type,
            UnityAction<BaseEventData> callBack)
        {
            // 这种逻辑主要是用于保证控件上只会挂载一个EventTrigger
            EventTrigger eventTrigger = control.GetComponent<EventTrigger>();

            if (eventTrigger == null)
                eventTrigger = control.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(callBack);

            eventTrigger.triggers.Add(entry);
        }

        /// <summary>
        /// 移除控件的指定类型事件监听器
        /// </summary>
        /// <param name="control">对应的控件</param>
        /// <param name="type">事件的类型</param>
        /// <param name="callBack">响应的函数</param>
        public static void RemoveEventTriggerListener(UIBehaviour control, EventTriggerType type,
            UnityAction<BaseEventData> callBack)
        {
            EventTrigger eventTrigger = control.GetComponent<EventTrigger>();
            if (eventTrigger != null)
            {
                // 查找指定类型的事件监听器并移除
                for (int i = eventTrigger.triggers.Count - 1; i >= 0; i--)
                {
                    var trigger = eventTrigger.triggers[i];
                    if (trigger.eventID == type && trigger.callback != null &&
                        trigger.callback.GetPersistentEventCount() > 0)
                    {
                        trigger.callback.RemoveListener(callBack);
                    }
                }
            }
        }
    }
}