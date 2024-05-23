using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProFrameworkTest
{
    public class MoveGameObjectTest : MonoBehaviour
    {
        private void Update()
        {
            this.transform.Translate(this.transform.forward * 5f * Time.deltaTime);
        }
    }
}