using UnityEngine;
using UnityEngine.Events;

namespace ProFramework
{
    /// <summary>
    /// MonoBehaviourπ‹¿Ì∆˜
    /// </summary>
    public class ProMonoBehaviourManager : ProUnityAutoSingleton<ProMonoBehaviourManager>
    {
        private event UnityAction UpdateEvent;
        private event UnityAction FixedUpdateEvent;
        private event UnityAction LateUpdateEvent;

        public void AddUpdateListener(UnityAction updateAction)
        {
            UpdateEvent += updateAction;
        }

        public void RemoveUpdateListener(UnityAction updateAction)
        {
            UpdateEvent -= updateAction;
        }

        public void AddFixedUpdateListener(UnityAction fixedUpdateAction)
        {
            FixedUpdateEvent += fixedUpdateAction;
        }

        public void RemoveFixedUpdateListener(UnityAction fixedUpdateAction)
        {
            FixedUpdateEvent -= fixedUpdateAction;
        }

        public void AddLateUpdateListener(UnityAction lateUpdateAction)
        {
            LateUpdateEvent += lateUpdateAction;
        }

        public void RemoveLateUpdateListener(UnityAction lateUpdateAction)
        {
            LateUpdateEvent -= lateUpdateAction;
        }

        private void Update()
        {
            UpdateEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }

        private void LateUpdate()
        {
            LateUpdateEvent?.Invoke();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UpdateEvent = null;
            FixedUpdateEvent = null;
            LateUpdateEvent = null;
        }
    }
}