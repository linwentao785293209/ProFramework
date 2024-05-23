using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// 输入信息
    /// </summary>
    public class ProInputInfo
    {
        /// <summary>
        /// 输入的类型——抬起、按下、长按
        /// </summary>
        public EProInputType InputType { get; set; }

        /// <summary>
        /// 键码
        /// </summary>
        public KeyCode Key { get; set; }

        /// <summary>
        /// 初始化输入信息实例
        /// </summary>
        /// <param name="inputType">输入类型</param>
        /// <param name="key">键码</param>
        public ProInputInfo(EProInputType inputType, KeyCode key)
        {
            InputType = inputType;
            Key = key;
        }
    }
}
