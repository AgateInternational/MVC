using Framework.Architecture.Base;

namespace Game.Module.Currency
{
    public interface ICurrencyModel : IBaseModel
    {
        int Gold { get; }
    }

    public class CurrencyModel : BaseModel, ICurrencyModel
    {
        public int Gold { get; protected set; } = 0;

        public void AddGold()
        {
            Gold++;
            SetDataAsDirty();
        }
    }
}
