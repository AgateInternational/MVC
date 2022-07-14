using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Example.Scene.Idle.Timer
{
    public class TimerView : ObjectView<ITimerModel>
    {
        [SerializeField]
        private Text _remainingDuration;
        private Action _onUpdate;

        public void Init(Action onUpdate)
        {
            _onUpdate = onUpdate;
        }

        private void Update()
        {
            _onUpdate?.Invoke();
        }

        protected override void InitRenderModel(ITimerModel model)
        {

        }

        protected override void UpdateRenderModel(ITimerModel model)
        {

        }
    }
}
