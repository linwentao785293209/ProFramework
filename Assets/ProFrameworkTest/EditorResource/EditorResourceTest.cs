using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProFramework;
using UnityEditor;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ProFrameworkTest
{
    public class EditorResourceTest : MonoBehaviour
    {
        public RawImage rawImage; // 用于显示图片的 RawImage 组件
        public Image image; // 用于显示图片的 RawImage 组件

        private List<GameObject> GOList = new List<GameObject>();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // // 加载预设体
                // GameObject prefab =
                //     ProEditorResourceManager.Instance.LoadEditorResource<GameObject>(
                //         $"{ProConst.Models}/CubeTest");
                // if (prefab != null)
                // {
                //     GOList.Add(Instantiate(prefab, transform.position, Quaternion.identity));
                // }

                ProEditorResourceManager.Instance.LoadResource<GameObject>(ProConst.Models, "CubeTest", (prefab) =>
                {
                    if (prefab != null)
                    {
                        GOList.Add(Instantiate(prefab, transform.position, Quaternion.identity));
                    }
                });
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                // 加载材质球
                Material material =
                    ProEditorResourceManager.Instance.LoadEditorResource<Material>(
                        $"{ProConst.Materials}/RedMaterialTest");
                if (material != null)
                {
                    foreach (var go in GOList)
                    {
                        go.GetComponent<Renderer>().material = material;
                    }
                }
            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                // 加载纹理
                Texture texture =
                    ProEditorResourceManager.Instance.LoadEditorResource<Texture>($"{ProConst.Textures}/head");
                if (texture != null)
                {
                    rawImage.texture = texture; // 将加载的图片设置为 RawImage 的 texture
                }
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                Sprite sprite =
                    ProEditorResourceManager.Instance.LoadSpriteInSpriteAtlas($"{ProConst.Textures}/AtlasTest",
                        "head");
                if (sprite != null)
                {
                    image.sprite = sprite; // 将加载的图片设置为 RawImage 的 texture
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                Dictionary<string,Sprite> spriteDictionary = ProEditorResourceManager.Instance.LoadSpritesDictionaryInAtlas($"{ProConst.Textures}/AtlasTest");
                if (spriteDictionary != null && spriteDictionary.ContainsKey("folder"))
                {
                    image.sprite = spriteDictionary["folder"]; // 将加载的图片设置为 RawImage 的 texture
                }
            }
        }
    }
}