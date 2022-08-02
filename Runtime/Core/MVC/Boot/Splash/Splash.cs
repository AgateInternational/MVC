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

        protected abstract IMain GetMain();
        protected abstract ILoad GetLoader();
        protected abstract void StartSplash();
        protected abstract void FinishSplash();
        protected abstract void StartTransition();
        protected abstract void FinishTransition();
    }
}
