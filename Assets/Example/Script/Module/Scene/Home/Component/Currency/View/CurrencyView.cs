using Framework.Architecture.Base;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Module.Currency
{
    public class CurrencyView : ObjectView<ICurrencyModel>
    {
        [SerializeField]
        protected Text _goldLabel;
        [SerializeField]
        protected Button _addGoldButton;

        public void Init(UnityAction onAddGold)
        {
            _addGoldButton.onClick.RemoveAllListeners();
            _addGoldButton.onClick.AddListener(onAddGold);
        }

        protected override void InitRenderModel(ICurrencyModel model)
        {
            _goldLabel.text = model.Gold.ToString();
        }

        protected override void UpdateRenderModel(ICurrencyModel model)
        {
            _goldLabel.text = model.Gold.ToString();
        }
    }
}
