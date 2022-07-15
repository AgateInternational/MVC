using Agate.MVC.Base;
using Example.Scene.Idle.Timer;

namespace Example.Scene.Idle.Hero
{
    public interface IHeroModel : IBaseModel
    {
        public string Name { get; }
        public int Level { get; }
        public int Income { get; }
        public int Cost { get; }
    }

    public class HeroModel : BaseModel, IHeroModel
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Income { get; private set; }
        public int Cost { get; private set; }
        public int BaseIncome { get; private set; }
        public int BaseCost { get; private set; }
        public TimerModel Timer { get; private set; }

        public HeroModel() { }

        public HeroModel(string name, int baseIncome, int baseCost, int duration)
        {
            Name = name;
            Level = 1;
            BaseIncome = baseIncome;
            Income = BaseIncome;
            BaseCost = baseCost;
            Cost = BaseCost;
            Timer = new TimerModel(duration);
            SetDataAsDirty();
        }

        public void Upgrade()
        {
            Level++;
            Income = Level * BaseIncome;
            Cost = Level * BaseCost;
            SetDataAsDirty();
        }
    }
}
