using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Example.Boot
{
    public class GameLauncher : BaseMain<GameLauncher>, IMain
    {
        protected override IConnector[] GetConnectors()
        {
            return null;
        }

        protected override IController[] GetDependencies()
        {
            return null;
        }

        protected override IEnumerator StartInit()
        {
            CreateEventSystem();
            Application.targetFrameRate = 60;
            yield return null;
        }

        private void CreateEventSystem()
        {
            GameObject obj = new GameObject("Event System");
            obj.AddComponent<EventSystem>();
            obj.AddComponent<StandaloneInputModule>();
            GameObject.DontDestroyOnLoad(obj);
        }
    }
}
