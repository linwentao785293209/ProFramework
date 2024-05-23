using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class SceneTest1 : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ProSceneManager.Instance.LoadScene("SceneTest2", (() => { ProLog.LogDebug("ÇÐ»»µ½³¡¾°2"); }), true);
            }
        }
    }
}