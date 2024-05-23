using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;
using UnityEngine.UI;

namespace ProFrameworkTest
{
    public class AssetBundleTest : MonoBehaviour
    {
        public Image image;

        void Update()
        {
            // 检测按键
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ProAssetBundleManager.Instance.LoadResource<GameObject>(ProConst.Models, "CubeTest",
                    (gameObject => { Instantiate(gameObject); }));


                ProAssetBundleManager.Instance.LoadResource<GameObject>(ProConst.Models, "CubeTest",
                    (gameObject => { Instantiate(gameObject); }), true);
            }

            // 检测按键
            if (Input.GetKeyDown(KeyCode.W))
            {
                ProAssetBundleManager.Instance.UnLoadAssetBundle(ProConst.Materials,
                    (isSuccess) => { ProLog.LogDebug("卸载Materials包结果" + isSuccess); }, true);
            }

            // 检测按键
            if (Input.GetKeyDown(KeyCode.E))
            {
                ProAssetBundleManager.Instance.UnLoadAssetBundle(ProConst.Models,
                    (isSuccess) => { ProLog.LogDebug("卸载Model包结果" + isSuccess); }, true);
            }

            // 检测按键
            if (Input.GetKeyDown(KeyCode.R))
            {
                ProAssetBundleManager.Instance.LoadResource<Material>(ProConst.Materials,"RedMaterialTest",
                    (isSuccess) => { ProLog.LogDebug("RedMaterialTest加载结果" + isSuccess); }, true);
            }
            

            if (Input.GetKeyDown(KeyCode.T))
            {
                ProAssetBundleManager.Instance.LoadResource<Sprite>(ProConst.Textures, "head",
                    (sprite => { image.sprite = sprite; }));
            }
        }
    }
}