using System.Collections;

namespace Agate.MVC.Core
{
    public enum InitializeState
    {
        NotInitialized = 0, Initializing = 1, Initialized,
    }

    public abstract class Main<T> : SingletonBehaviour<T> where T : Main<T>, IMain
    {
        #region Interface Implementation
        public event InitializeProgress OnInitializing;
        public event InitializeEvent OnInitializeStart;
        public event InitializeEvent OnInitializeFinish;
        public InitializeState State { get; protected set; }
        public bool IsInitialized { get { return State == InitializeState.Initialized; } }
        #endregion

        #region Initialize Process
        public void InitMain()
        {
            if (State == InitializeState.NotInitialized)
            {
                StartCoroutine(Initialize());
            }
        }

        protected virtual IEnumerator Initialize()
        {
            if (State == InitializeState.NotInitialized)
            {
                State = InitializeState.Initializing;

                if (OnInitializeStart != null)
                {
                    OnInitializeStart();
                }

                yield return StartInit();
                yield return InitDependencies();
                yield return FinalizeInit();

                State = InitializeState.Initialized;

                if (OnInitializeFinish != null)
                {
                    OnInitializeFinish();
                }
            }
        }

        protected virtual IEnumerator InitDependencies()
        {
            var systems = GetDependencies();
            if (systems != null)
            {
                int count = systems.Length;
                for (int i = 0; i < count; i++)
                {
                    yield return systems[i].Initialize();
                }

                for (int i = 0; i < count; i++)
                {
                    yield return systems[i].Finalize();

                    if (OnInitializing != null)
                    {
                        OnInitializing(i + 1, count);
                    }
                    yield return null;
                }
            }
            yield return null;
        }
        #endregion


        #region Abstract Method
        protected abstract IController[] GetDependencies();
        protected abstract IEnumerator StartInit();
        protected abstract IEnumerator FinalizeInit();
        #endregion
    }
}
