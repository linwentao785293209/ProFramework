using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

namespace ProFramework
{
    /// <summary>
    /// 编辑器资源管理器
    /// 注意：只有在开发时能使用该管理器加载资源 用于开发功能
    /// 发布后 是无法使用该管理器的 因为它需要用到编辑器相关功能
    /// </summary>
    public class ProEditorResourceManager : ProSingletonInSystem<ProEditorResourceManager>, IProLoadResourceManager
    {
        //用于放置需要打包进AB包中的资源路径 
        private string editorAssetBundle => ProConst.EditorAssetBundlePath;

        private ProEditorResourceManager()
        {
        }

        public void LoadResource<T>(string assetBundleName, string resourceName, UnityAction<T> callBack = null,
            bool isSync = false) where T : Object
        {
        #if UNITY_EDITOR
            string suffixName = "";
            //预设体、纹理（图片）、材质球、音效等等
            if (typeof(T) == typeof(GameObject))
                suffixName = ".prefab";
            else if (typeof(T) == typeof(Material))
                suffixName = ".mat";
            else if (typeof(T) == typeof(Texture))
                suffixName = ".png";
            else if (typeof(T) == typeof(AudioClip))
                suffixName = ".mp3";
            else if (typeof(T) == typeof(SpriteAtlas))
                suffixName = ".spriteatlas";

            string path = $"{editorAssetBundle}{assetBundleName}/{resourceName}{suffixName}";

            LoadEditorResource<T>(path, callBack);
        #else
                ProLog.LogError("非编辑器模式下不能用编辑器管理器加载资源！返回空！");
                return null;
        #endif
        }

        //1.加载单个资源的
        public T LoadEditorResource<T>(string path, UnityAction<T> callBack = null) where T : Object
        {
        #if UNITY_EDITOR
            T res = AssetDatabase.LoadAssetAtPath<T>(path);

            if (res == null)
            {
                ProLog.LogWarning($"编辑器资源中未找到{path}路径下的{typeof(T)}类型资源！返回空！");
            }


            callBack(res);


            return res;
        #else
                ProLog.LogError("非编辑器模式下不能用编辑器管理器加载资源！返回空！");
                return null;
        #endif
        }

        //2.加载图集相关资源的
        public Sprite LoadSpriteInSpriteAtlas(string atlasPath, string spriteName)
        {
        #if UNITY_EDITOR
            // 应当加载整个Sprite Atlas，而非所有子资源
            SpriteAtlas spriteAtlas =
                AssetDatabase.LoadAssetAtPath<SpriteAtlas>(editorAssetBundle + atlasPath + ".spriteatlas");

            if (spriteAtlas == null)
            {
                ProLog.LogWarning($"编辑器资源中未找到{atlasPath}路径下的图集资源！返回空！");
                return null;
            }

            // 从Sprite Atlas中获取指定名字的Sprite
            Sprite resultSprite = spriteAtlas.GetSprite(spriteName);

            if (resultSprite == null)
            {
                ProLog.LogWarning($"编辑器资源中找到{atlasPath}路径下的图集资源，但是未找到图集下名为{spriteName}的纹理，返回空！");
            }

            return resultSprite;
        #else
            ProLog.LogError("非编辑器模式下不能用编辑器管理器加载资源！返回空！");
            return null;
        #endif
        }


        //加载图集文件中的所有子图片并返回给外部
//加载图集文件中的所有子图片并返回给外部
        public Dictionary<string, Sprite> LoadSpritesDictionaryInAtlas(string atlasPath)
        {
        #if UNITY_EDITOR
            Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

            // 加载Sprite Atlas
            SpriteAtlas spriteAtlas =
                AssetDatabase.LoadAssetAtPath<SpriteAtlas>(editorAssetBundle + atlasPath + ".spriteatlas");

            if (spriteAtlas == null)
            {
                ProLog.LogWarning($"编辑器资源中未找到{atlasPath}路径下的图集资源！返回空！");
                return null;
            }

            // 获取图集中的所有精灵
            Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
            spriteAtlas.GetSprites(sprites);

            // 将精灵添加到字典中
            foreach (var sprite in sprites)
            {
                // 去掉 "(Clone)" 后缀
                string spriteName = sprite.name.Replace("(Clone)", "").Trim();
                if (!spriteDic.ContainsKey(spriteName))
                {
                    spriteDic.Add(spriteName, sprite);
                }
                else
                {
                    // 如果字典中已经存在相同名称的精灵，可以选择添加一个后缀或者忽略该精灵
                    // 这里选择添加一个后缀
                    int index = 1;
                    while (spriteDic.ContainsKey(spriteName))
                    {
                        spriteName = $"{sprite.name.Replace("(Clone)", "")} ({index})";
                        index++;
                    }

                    ProLog.LogWarning($"编辑器资源中未找到{atlasPath}路径下的图集资源中的图存在同名资源，字典中的key添加后缀{index},key为{spriteName}");
                    spriteDic.Add(spriteName, sprite);
                }
            }

            return spriteDic;
        #else
            ProLog.LogError("非编辑器模式下不能用编辑器管理器加载资源！返回空！");
            return null;
        #endif
        }
    }
}