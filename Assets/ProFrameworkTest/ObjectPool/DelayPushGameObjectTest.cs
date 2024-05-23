using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class DelayPushGameObjectTest : MonoBehaviour
    {
        void OnEnable()
        {
            Invoke("DelayPushGameObject",3);
        }

        void DelayPushGameObject()
        {
            ProGameObjectPoolManager.Instance.Push(this.gameObject);
        }
    }
}