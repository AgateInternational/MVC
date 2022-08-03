using UnityEngine;

namespace Agate.MVC.Core
{
    public abstract class Splash<T> : SingletonBehaviour<T> where T : Splash<T>, ISplash
    {
        public bool IsInitialized { get; protected set; } = false;

        public virtual void InitSplash()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;

                IMain main = GetMain();
                main.OnInitializeStart += StartSplash;
                main.OnInitializeFinish += FinishSplash;

                ILoad loader = GetLoader();
                loader.OnStartTransition += StartTransition;
                loader.OnFinishTransition += FinishTransition;
            }
        }

        protected virtual void StartSplash()
        {
            this.gameObject.SetActive(true);
        }

        protected virtual void FinishSplash()
        {
            this.gameObject.SetActive(false);
        }

        protected virtual void StartTransition()
        {
            this.gameObject.SetActive(true);
        }

        protected virtual void FinishTransition()
        {
            this.gameObject.SetActive(false);
        }

        #region Abstract
        protected abstract IMain GetMain();
        protected abstract ILoad GetLoader();
        #endregion
    }
}
