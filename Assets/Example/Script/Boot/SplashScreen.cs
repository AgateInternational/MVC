using Agate.MVC.Base;
using Agate.MVC.Core;
using UnityEngine;

namespace Example.Boot
{
    public class SplashScreen : BaseSplash<SplashScreen>
    {
        [SerializeField]
        private GameObject _splashUI;
        [SerializeField]
        private GameObject _transitionUI;

        protected override IMain GetMain()
        {
            return GameMain.Instance;
        }

        protected override ILoad GetLoader()
        {
            return SceneLoader.Instance;
        }

        protected override void StartSplash()
        {
            base.StartSplash();
            _splashUI.SetActive(true);
            _transitionUI.SetActive(false);
        }

        protected override void FinishSplash()
        {
            base.FinishSplash();
            _splashUI.SetActive(false);
        }

        protected override void StartTransition()
        {
            base.StartTransition();
            _transitionUI.SetActive(true);
        }

        protected override void FinishTransition()
        {
            base.FinishTransition();
            _transitionUI.SetActive(false);
        }

    }
}
