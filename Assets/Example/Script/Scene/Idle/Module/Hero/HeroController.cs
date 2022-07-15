using Agate.MVC.Base;
using Example.Scene.Idle.Gold;
using Example.Scene.Idle.Timer;
using System;

namespace Example.Scene.Idle.Hero
{
    public class HeroController : ObjectController<HeroController, HeroModel, IHeroModel, HeroView>
    {
        private GoldController _gold;

        public void Init(HeroModel model, HeroView view, TimerView timer)
        {
            _model = model;
            SetView(view);
            _view.Init(Upgrade);
            timer.SetModel(_model.Timer);
            timer.Init(TickTimer);
            StartTimer();
        }

        private void Upgrade()
        {
            if (_gold.SpendGold(_model.Cost))
            {
                _model.Upgrade();
            }
        }

        private void StartTimer()
        {
            _model.Timer.StartTimer(GetCurrentTime());
        }

        private void TickTimer()
        {
            long currentTime = GetCurrentTime();
            _model.Timer.UpdateTimer(currentTime);
            if (_model.Timer.IsCompleted)
            {
                Publish(new EarnGoldMessage(_model.Income));
                _model.Timer.StartTimer(currentTime);
            }
        }

        private long GetCurrentTime()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}
