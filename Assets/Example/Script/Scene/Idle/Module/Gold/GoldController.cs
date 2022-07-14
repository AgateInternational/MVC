using Agate.MVC.Base;

namespace Example.Scene.Idle.Gold
{
    public class GoldController : ObjectController<GoldController, GoldModel, IGoldModel, GoldView>
    {
        public bool SpendGold(int value)
        {
            return _model.Spend(value);
        }

        public void AddGold(int value)
        {
            _model.Add(value);
        }
    }
}
