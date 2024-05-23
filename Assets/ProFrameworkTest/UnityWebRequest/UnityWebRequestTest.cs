using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;
using UnityEngine.UI;

namespace ProFrameworkTest
{
    public class UnityWebRequestTest : MonoBehaviour
    {
        public RawImage rawImage;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ProUnityWebRequestManager.Instance.LoadResource<string>(
                    "file://" + Application.streamingAssetsPath + "/ProFramework/Test/test.txt",
                    (str) => { ProLog.LogDebug($"文本信息是{str}"); }, (() => { ProLog.LogDebug($"加载失败"); }));
            }


            if (Input.GetKeyDown(KeyCode.W))
            {
                ProUnityWebRequestManager.Instance.LoadResource<byte[]>(
                    "file://" + Application.streamingAssetsPath + "/ProFramework/Test/test.txt",
                    (bytes) => { ProLog.LogDebug($"字节数组长度是{bytes.Length}"); }, (() => { ProLog.LogDebug($"加载失败"); }));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ProUnityWebRequestManager.Instance.LoadResource<Texture>(
                    "file://" + Application.streamingAssetsPath + "/ProFramework/Test/head.png",
                    (texture) => { rawImage.texture = texture; }, (() => { ProLog.LogDebug($"加载失败"); }));
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ProUnityWebRequestManager.Instance.LoadResource<AssetBundle>(
                    "file://" + Application.streamingAssetsPath + "/ProFramework/AssetBundle/PC/audios",
                    (assetBundle) => { ProLog.LogDebug($"{assetBundle.name}"); }, (() => { ProLog.LogDebug($"加载失败"); }));
            }
        }
    }
}