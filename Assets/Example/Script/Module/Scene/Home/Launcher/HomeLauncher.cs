using System.Collections;
using Framework.Architecture.Base;
using Framework.Architecture.Pattern.MVC;
using Game.Boot;
using UnityEngine;
using Game.Module.Currency;

namespace Game.Scene.Home
{
    public class HomeLauncher : SceneLauncher<HomeLauncher, HomeView>
    {
        [SerializeField]
        private HomeView _homeView;
        private CurrencyController _currency;

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[]{
            };
        }

        protected override IController[] GetSceneDependencies()
        {
            return new IController[]{
                new CurrencyController()
            };
        }

        protected override string GetSceneName()
        {
            return "Home";
        }

        protected override HomeView GetSceneView()
        {
            return _homeView;
        }

        protected override IEnumerator InitSceneObject()
        {
            _currency.SetView(_view.Currency);
            yield return null;
        }

        protected override IEnumerator LaunchScene()
        {
            yield return null;
        }
    }
}
