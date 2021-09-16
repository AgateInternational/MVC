using System.Collections;
using Framework.Architecture.Base;

namespace Game.Module.Currency
{
    public class CurrencyController : ObjectController<CurrencyController, CurrencyModel, ICurrencyModel, CurrencyView>
    {
        public override IEnumerator Initialize()
        {
            return base.Initialize();

            // load dari save data
        }

        public override void SetView(CurrencyView view)
        {
            base.SetView(view);
            _view.Init(AddGold);
        }

        public void AddGold()
        {
            _model.AddGold();
            Publish<CurrencyMessage>(new CurrencyMessage(_model.Gold));
        }
    }
}
