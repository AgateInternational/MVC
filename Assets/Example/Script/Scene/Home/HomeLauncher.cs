using Example.Boot;
using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;

namespace Example.Scene.Home
{
    public class HomeLauncher : SceneLauncher<HomeLauncher, HomeView>
    {
        public override string SceneName {get {return "Home";}}

        protected override IController[] GetSceneDependencies()
        {
            return null;
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return null;
        }

        protected override IEnumerator LaunchScene()
        {
            yield return null;
        }

        protected override IEnumerator InitSceneObject()
        {
            _view.SetButtonCallback(GoToGameplay);
            yield return null;
        }

        private void GoToGameplay()
        {
            SceneLoader.Instance.LoadScene("Idle");
        }
    }
}
