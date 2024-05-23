using System.Collections;
using System.Collections.Generic;
using ProFramework;
using ProFrameworkTest;
using UnityEngine;

public class UGUITest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ProUGUIManager.Instance.ShowPanel<UGUITestPanel1>();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ProUGUIManager.Instance.HidePanel<UGUITestPanel1>();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            ProUGUIManager.Instance.ShowPanel<UGUITestPanel2>(EProUGUILayer.Middle);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ProUGUIManager.Instance.HidePanel<UGUITestPanel2>();
        }
    }
}