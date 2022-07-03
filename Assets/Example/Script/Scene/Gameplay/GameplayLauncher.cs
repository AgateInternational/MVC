using Example.Boot;
using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;

namespace Example.Scene.Gameplay
{
    public class GameplayLauncher : SceneLauncher<GameplayLauncher, GameplayView>
    {
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
            _view.SetButtonCallback(GoToHome);
            yield return null;
        }

        private void GoToHome()
        {
            SceneLoader.Instance.LoadScene("Home");
        }
    }
}
