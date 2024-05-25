using System;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class PlayerPrefsTest : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayerPrefsTestClass playerPrefsTestClass1 = new PlayerPrefsTestClass();
                ProPlayerPrefsDataManager.Instance.Save<PlayerPrefsTestClass>("myPlayerPrefsTestClass",
                    playerPrefsTestClass1);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                PlayerPrefsTestClass playerPrefsTestClass2 =
                    ProPlayerPrefsDataManager.Instance.Load<PlayerPrefsTestClass>("myPlayerPrefsTestClass");
            }
        }
    }
}