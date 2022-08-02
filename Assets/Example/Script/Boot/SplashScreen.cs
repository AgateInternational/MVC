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
            this.gameObject.SetActive(true);
            _splashUI.SetActive(true);
            _transitionUI.SetActive(false);
        }

        protected override void FinishSplash()
        {
            this.gameObject.SetActive(false);
            _splashUI.SetActive(false);
        }

        protected override void StartTransition()
        {
            this.gameObject.SetActive(true);
            _transitionUI.SetActive(true);
        }

        protected override void FinishTransition()
        {
            this.gameObject.SetActive(false);
            _transitionUI.SetActive(false);
        }

    }
}
