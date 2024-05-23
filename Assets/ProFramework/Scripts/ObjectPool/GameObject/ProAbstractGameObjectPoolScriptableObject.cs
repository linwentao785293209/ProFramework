using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 抽象的游戏对象池脚本ableObject，用于定义最大数量。
    /// </summary>
    public abstract class ProAbstractGameObjectPoolScriptableObject : ScriptableObject
    {
        /// <summary>
        /// 最大对象数量。
        /// </summary>
        public int MaxNum;
    }
}