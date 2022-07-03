using Example.Boot;
using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Example.Scene.Gameplay
{
    public class GameplayView : BaseSceneView
    {
        [SerializeField]
        private Button _button;

        public void SetButtonCallback(UnityAction callback)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(callback);
        }
    }
}
