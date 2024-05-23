using System.Collections;
using System.Collections.Generic;
using ProFramework;
using UnityEngine;

namespace ProFrameworkTest
{
    public class EventTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            ProEventManager.Instance.AddEventListener(EMyEventType.Skill1, TestSkill1);
            ProEventManager.Instance.AddEventListener<float>(EMyEventType.Skill2, TestSkill2);
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ProEventManager.Instance.EventTrigger(EMyEventType.Skill1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                ProEventManager.Instance.EventTrigger(EMyEventType.Skill2, Random.Range(0f, 100000f));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ProEventManager.Instance.RemoveEventListener(EMyEventType.Skill1, TestSkill1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ProEventManager.Instance.RemoveEventListener<float>(EMyEventType.Skill2, TestSkill2);
            }
        }

        void TestSkill1()
        {
            ProLog.LogDebug("技能1释放");
        }

        void TestSkill2(float damage)
        {
            ProLog.LogDebug("技能2释放", $"造成伤害{damage}");
        }
    }
}