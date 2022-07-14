using Agate.MVC.Base;

namespace Example.Scene.Idle.Gold
{
    public interface IGoldModel : IBaseModel
    {
        public int Current { get; }
    }

    public class GoldModel : BaseModel, IGoldModel
    {
        public int Current { get; private set; } = 0;

        public GoldModel() { }

        public void Add(int value)
        {
            Current += value;
            SetDataAsDirty();
        }

        public bool Spend(int value)
        {
            if (Current >= value)
            {
                Current -= value;
                SetDataAsDirty();
                return true;
            }
            return false;
        }
    }
}
