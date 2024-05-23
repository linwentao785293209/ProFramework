using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class SceneTest2 : MonoBehaviour
    {
        void Start()
        {
            ProEventManager.Instance.AddEventListener<float>(EProEventType.OnSceneChange,
                (value) => { ProLog.LogDebug($"加载进度为{value}"); });
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ProSceneManager.Instance.LoadScene("SceneTest1", (() => { ProLog.LogDebug("切换到场景1"); }));
            }
        }
    }
}