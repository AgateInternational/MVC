using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Example.Scene.Idle.Tavern
{
    public class TavernView : ObjectView<ITavernModel>
    {
        [SerializeField]
        private RectTransform _heroContainer;
        [SerializeField]
        private GameObject _heroTemplate;
        [SerializeField]
        private Button _buyButton;
        [SerializeField]
        private Text _buyPrice;

        public void Init(UnityAction onBuy)
        {
            _buyButton.onClick.RemoveAllListeners();
            _buyButton.onClick.AddListener(onBuy);
        }

        public GameObject CreateHeroObject(string objectName)
        {
            GameObject obj = GameObject.Instantiate(_heroTemplate, _heroContainer);
            obj.name = objectName;
            obj.SetActive(true);
            return obj;
        }

        protected override void InitRenderModel(ITavernModel model)
        {
        }

        protected override void UpdateRenderModel(ITavernModel model)
        {
            if (model.BuyPrice > 0)
            {
                _buyPrice.text = model.BuyPrice.ToString();
            }
            else
            {
                _buyPrice.text = "FREE";
            }
        }

    }
}
