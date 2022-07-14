using Example.Boot;
using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Example.Scene.Idle.Gold;
using Example.Scene.Idle.Tavern;

namespace Example.Scene.Idle
{
    public class IdleView : BaseSceneView
    {
        public TavernView Tavern {get {return _tavern;}}
        public GoldView Gold {get {return _gold;}}

        [SerializeField]
        private TavernView _tavern;
        [SerializeField]
        private GoldView _gold;
        [SerializeField]
        private Text _sceneName;
        [SerializeField]
        private Button _homeButton;
        [SerializeField]
        private Button _restartButton;

        public void Init(string sceneName, UnityAction onHome, UnityAction onRestart)
        {
            _sceneName.text = sceneName;

            _homeButton.onClick.RemoveAllListeners();
            _homeButton.onClick.AddListener(onHome);

            _restartButton.onClick.RemoveAllListeners();
            _restartButton.onClick.AddListener(onRestart);
        }
       
    }
}
