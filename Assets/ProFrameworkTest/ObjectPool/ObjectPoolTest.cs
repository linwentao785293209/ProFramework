using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class ObjectPoolTest : MonoBehaviour
    {
        private TestProSystemObject systemObject;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ProGameObjectPoolManager.Instance.Get(ProConst.Models, "CubeTest",
                    (go => { go.transform.position = Vector3.zero; }));
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                ProGameObjectPoolManager.Instance.Get(ProConst.Models, "SphereTest",
                    (go => { go.transform.position = Vector3.zero; }));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                systemObject = ProSystemObjectPoolManager.Instance.Get<TestProSystemObject>();
                systemObject.num = Random.Range(1, 99999);
                ProLog.LogDebug(systemObject.num);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ProSystemObjectPoolManager.Instance.Push(systemObject);
            }
        }
    }
}