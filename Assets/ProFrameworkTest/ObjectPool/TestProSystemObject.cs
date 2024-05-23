using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class TestProSystemObject : IProSystemObject
    {
        public int num;

        public void ResetInfo()
        {
            num = 0;
        }
    }
}