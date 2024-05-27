using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;
using UnityEngine.UI;

namespace ProFrameworkTest
{
    public class ResourceTest : MonoBehaviour
    {
        public RawImage rawImage; // 用于显示图片的 RawImage 组件
        public List<GameObject> capsuleTestList = new List<GameObject>(); // 存储加载的 Prefab 实例的列表

        void Update()
        {
            // 按下 Q 键，同步加载预制体资源
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ProResourceManager.Instance.LoadResource<GameObject>(ProConst.Models, "CapsuleTest", OnCapsuleTestLoaded, true);
            }

            // 按下 W 键，异步加载预制体资源
            if (Input.GetKeyDown(KeyCode.W))
            {
                ProResourceManager.Instance.LoadResource<GameObject>(ProConst.Models, "CapsuleTest", (capsuleTest) =>
                {
                    if (capsuleTest != null)
                    {
                        GameObject obj = Instantiate(capsuleTest); // 实例化加载的预制体
                        capsuleTestList.Add(obj); // 添加到列表中
                    }
                }, false);
            }

            // 按下 E 键，卸载预制体资源
            if (Input.GetKeyDown(KeyCode.E))
            {
                ProResourceManager.Instance.UnloadAsset<GameObject>(
                    $"{ProConst.Models}/" + "CapsuleTest", true);
            }

            // 按下 R 键，销毁场景中所有已加载的预制体
            if (Input.GetKeyDown(KeyCode.R))
            {
                foreach (var capsuleTest in capsuleTestList)
                {
                    Destroy(capsuleTest);
                }

                capsuleTestList.Clear(); // 清空列表
            }

            // 按下 A 键，异步加载头像图片资源
            if (Input.GetKeyDown(KeyCode.A))
            {
                ProResourceManager.Instance.LoadAsync<Texture>(
                    $"{ProConst.Textures}/" + "head", (headImg) =>
                    {
                        if (headImg != null)
                        {
                            rawImage.texture = headImg; // 将加载的图片设置为 RawImage 的 texture
                        }
                    });
            }

            // 按下 S 键，卸载头像图片资源
            if (Input.GetKeyDown(KeyCode.S))
            {
                ProResourceManager.Instance.UnloadAsset<Texture>(
                    $"{ProConst.Textures}/" + "head", true); // 卸载资源
                rawImage.texture = null; // 将 RawImage 的 texture 设置为 null
            }

            // 按下 D 键，异步加载文件夹图片资源
            if (Input.GetKeyDown(KeyCode.D))
            {
                ProResourceManager.Instance.LoadAsync<Texture>(
                    $"{ProConst.Textures}/" + "folder", (folderImg) =>
                    {
                        if (folderImg != null)
                        {
                            rawImage.texture = folderImg; // 将加载的图片设置为 RawImage 的 texture
                        }
                    });
            }

            // 按下 F 键，卸载文件夹图片资源
            if (Input.GetKeyDown(KeyCode.F))
            {
                ProResourceManager.Instance.UnloadAsset<Texture>(
                    $"{ProConst.Textures}/" + "folder", true); // 卸载资源
                rawImage.texture = null; // 将 RawImage 的 texture 设置为 null
            }

            // 模拟清空资源
            if (Input.GetKeyDown(KeyCode.T))
            {
                ProResourceManager.Instance.Clear(OnResourceCleared); // 清空资源
            }
        }

        // 当预制体加载完成后的回调
        void OnCapsuleTestLoaded(GameObject capsuleTest)
        {
            if (capsuleTest != null)
            {
                GameObject obj = Instantiate(capsuleTest); // 实例化加载的预制体
                capsuleTestList.Add(obj); // 添加到列表中
            }
        }

        // 当资源清空完成后的回调
        void OnResourceCleared()
        {
            ProLog.LogDebug("清空字典，所有未使用资源，一般过场景"); // 输出日志
        }
    }
}