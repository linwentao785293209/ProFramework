using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class TestSystemSingletonManager : ProSingletonInSystem<TestSystemSingletonManager>
    {
        private TestSystemSingletonManager()
        {
            if (!InstanceIsNull)
            {
                ProLog.LogError("已经存在单例对象，不能重复创建");
                return;
            }
        }

        // 添加一个测试方法
        public void TestMethod()
        {
            ProLog.LogDebug("系统单例基类测试方法");
        }
    }
}