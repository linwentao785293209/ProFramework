using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class AssetTest : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ProAssetManager.Instance.LoadResource<GameObject>(ProConst.Models, "CubeTest",
                    OnResourceLoaded);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                ProAssetManager.Instance.LoadResource<GameObject>(ProConst.Models, "SphereTest",
                    OnResourceLoaded);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                ProAssetManager.Instance.LoadResource<GameObject>(ProConst.Models, "CylinderTest",
                    OnResourceLoaded);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ProAssetManager.Instance.LoadResource<GameObject>(ProConst.Models, "CapsuleTest",
                    OnResourceLoaded);
            }
        }


        private void OnResourceLoaded(GameObject resource)
        {
            if (resource != null)
            {
                Debug.Log($"Resource {resource.name} loaded successfully!");
                Instantiate(resource);
            }
            else
            {
                Debug.Log("Failed to load resource!");
            }
        }
    }
}