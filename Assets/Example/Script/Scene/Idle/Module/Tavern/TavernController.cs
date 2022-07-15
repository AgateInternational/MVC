using Agate.MVC.Base;
using Example.Scene.Idle.Gold;
using Example.Scene.Idle.Hero;
using Example.Scene.Idle.Timer;
using UnityEngine;

namespace Example.Scene.Idle.Tavern
{
    public class TavernController : ObjectController<TavernController, TavernModel, ITavernModel, TavernView>
    {
        private GoldController _gold;

        public override void SetView(TavernView view)
        {
            base.SetView(view);
            _view.Init(BuyHero);
        }

        private void BuyHero()
        {
            if (_gold.SpendGold(_model.BuyPrice))
            {
                SummonHero();
            }
        }

        private void SummonHero()
        {
            int heroId = _model.HeroCount + 1;
            int baseCost = 1 * heroId;
            int baseIncome = 2 * heroId;
            int duration = 1 * heroId;
            
            HeroModel heroModel = new HeroModel($"Hero {heroId}", baseIncome, baseCost, duration);
            GameObject obj = _view.CreateHeroObject(heroModel.Name);
            HeroView heroView = obj.GetComponent<HeroView>();
            TimerView timerView = obj.GetComponent<TimerView>();
            
            HeroController hero = new HeroController();
            InjectDependencies(hero);
            
            hero.Init(heroModel, heroView, timerView);
            _model.AddHero(hero);
        }
    }
}
