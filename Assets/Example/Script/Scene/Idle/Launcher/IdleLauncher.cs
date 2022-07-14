using Example.Boot;
using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using Example.Scene.Idle.Gold;
using Example.Scene.Idle.Tavern;

namespace Example.Scene.Idle
{
    public class IdleLauncher : SceneLauncher<IdleLauncher, IdleView>
    {
        public override string SceneName { get { return "Idle"; } }

        private GoldController _gold;
        private TavernController _tavern;

        protected override IController[] GetSceneDependencies()
        {
            return new IController[]{
                new GoldController(),
                new TavernController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[]{
                new IdleConnector()
            };
        }

        protected override IEnumerator LaunchScene()
        {
            yield return null;
        }

        protected override IEnumerator InitSceneObject()
        {
            _view.Init(SceneName, BackToHome, Restart);
            _gold.SetView(_view.Gold);
            _tavern.SetView(_view.Tavern);
            yield return null;
        }

        private void BackToHome()
        {
            SceneLoader.Instance.LoadScene("Home");
        }

        private void Restart()
        {
            SceneLoader.Instance.RestartScene();
        }
    }
}
