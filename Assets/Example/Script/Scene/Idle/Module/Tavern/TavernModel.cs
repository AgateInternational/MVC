using Agate.MVC.Base;
using System.Collections.Generic;
using Example.Scene.Idle.Hero;

namespace Example.Scene.Idle.Tavern
{
    public interface ITavernModel : IBaseModel
    {
        public int BuyPrice { get; }
    }

    public class TavernModel : BaseModel, ITavernModel
    {
        public TavernModel() { }

        public int BuyPrice { get; private set; }
        public int HeroCount { get; private set; }
        private List<HeroController> Heroes = new List<HeroController>();

        public void AddHero(HeroController hero)
        {
            Heroes.Add(hero);
            HeroCount++;
            BuyPrice = 50 * HeroCount;
            SetDataAsDirty();
        }

    }
}
