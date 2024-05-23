using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;
using UnityEngine.UI;

namespace ProFrameworkTest
{
    public class UGUITestPanel2 : ProUGUIPanel
    {
        protected override void OnToggleValueChange(string toggleName, bool value)
        {
            base.OnToggleValueChange(toggleName, value);

            switch (toggleName)
            {
                case "Toggle1":
                case "Toggle2":
                case "Toggle3":
                    if (value)
                    {
                        ProLog.LogDebug($"toggle组中{toggleName}的开关值为{value}");
                    }
                    break;
                case "MyToggle":
                    ProLog.LogDebug($"修改{toggleName}开关值为{value}");
                    break;
            }
        }

        protected override void OnSliderValueChange(string sliderName, float value)
        {
            base.OnSliderValueChange(sliderName, value);
            switch (sliderName)
            {
                case "MySlider":
                    ProLog.LogDebug($"修改{sliderName}开关值为{value}");
                    break;
            }
        }

        protected override void OnStart()
        {
          
        }

        internal override void OnShow()
        {
            GetUIControl<Toggle>("Toggle3").isOn = true;
        }

        internal override void OnHide()
        {
      
        }

        protected override void OnDispose()
        {
         
        }
    }
}

