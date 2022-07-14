using Agate.MVC.Base;
using UnityEngine.UI;
using UnityEngine;

namespace Example.Scene.Idle.Gold
{
    public class GoldView : ObjectView<IGoldModel>
    {
        [SerializeField]
        private Text _currentGold;

        protected override void InitRenderModel(IGoldModel model)
        {
            _currentGold.text = model.Current.ToString();
        }

        protected override void UpdateRenderModel(IGoldModel model)
        {
            _currentGold.text = model.Current.ToString();
        }
    }
}
