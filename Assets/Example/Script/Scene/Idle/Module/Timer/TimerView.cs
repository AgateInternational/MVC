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
        [SerializeField]
        private Image _progressBar;
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
            _remainingDuration.text = ((float) model.Remaining / 1000.0f).ToString("F2");
            _progressBar.fillAmount = model.Progress;
        }
    }
}
