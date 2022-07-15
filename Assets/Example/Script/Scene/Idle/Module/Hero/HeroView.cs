using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Example.Scene.Idle.Hero
{
    public class HeroView : ObjectView<IHeroModel>
    {
        [SerializeField]
        private Text _heroName;
        [SerializeField]
        private Text _heroLevel;
        [SerializeField]
        private Text _heroIncome;
        [SerializeField]
        private Text _upgradeCost;
        [SerializeField]
        private Button _upgradeButton;

        public void Init(UnityAction onUpgrade)
        {
            _upgradeButton.onClick.RemoveAllListeners();
            _upgradeButton.onClick.AddListener(onUpgrade);
        }

        protected override void InitRenderModel(IHeroModel model)
        {
            _heroName.text = model.Name;
        }

        protected override void UpdateRenderModel(IHeroModel model)
        {
            _heroLevel.text = $"Lv {model.Level}";
            _heroIncome.text = model.Income.ToString();
            _upgradeCost.text = model.Cost.ToString();
        }
    }
}
