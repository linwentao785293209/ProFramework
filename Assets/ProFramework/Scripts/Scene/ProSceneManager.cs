using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProFramework
{
    /// <summary>
    /// 场景切换管理器 主要用于切换场景
    /// </summary>
    public class ProSceneManager : ProSingletonInSystem<ProSceneManager>
    {
        private ProSceneManager()
        {
        }


        public void LoadScene(string name, UnityAction callBack = null, bool isSync = false)
        {
            if (isSync)
            {
                LoadSceneSync(name, callBack);
            }
            else
            {
                LoadSceneAsync(name, callBack);
            }
        }

        //同步切换场景的方法
        private void LoadSceneSync(string name, UnityAction callBack = null)
        {
            //切换场景
            SceneManager.LoadScene(name);
            //调用回调
            callBack?.Invoke();
            callBack = null;
        }

        //异步切换场景的方法
        private void LoadSceneAsync(string name, UnityAction callBack = null)
        {
            ProMonoManager.Instance.StartCoroutine(LoadSceneCoroutine(name, callBack));
        }

        private IEnumerator LoadSceneCoroutine(string name, UnityAction callBack)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
            //不停的在协同程序中每帧检测是否加载结束 如果加载结束就不会进这个循环每帧执行了
            while (!asyncOperation.isDone)
            {
                //可以在这里利用事件中心 每一帧将进度发送给想要得到的地方
                ProEventManager.Instance.EventTrigger<float>(EProEventType.OnSceneChange, asyncOperation.progress);
                yield return 0;
            }

            //避免最后一帧直接结束了 没有同步1出去
            ProEventManager.Instance.EventTrigger<float>(EProEventType.OnSceneChange, 1);

            callBack?.Invoke();
            callBack = null;
        }
    }
}