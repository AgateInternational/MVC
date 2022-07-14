using Agate.MVC.Base;
using Example.Scene.Idle.Gold;
using Example.Scene.Idle.Timer;

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
            
        }

        private void TickTimer()
        {

        }
    }
}
