using System.Collections;
using UnityEngine;

namespace Agate.MVC.Core
{
    public enum SceneLoadState
    {
        NotLoaded = 0, Loading = 1, Loaded = 2, Unloading = 3
    }

    public abstract class Launcher<TView> : MonoBehaviour, ILauncher
        where TView : View
    {
        #region Interface Implementation
        public SceneLoadState State { get; protected set; }
        public abstract string SceneName { get; }
        #endregion

        [SerializeField]
        protected TView _view;
        protected IController[] _dependencies;

        protected void Awake()
        {
            StartCoroutine(AutoInitialize());
        }

        #region Auto Initialize
        protected virtual IEnumerator AutoInitialize()
        {
            ILoad loader = GetLoader();
            if (!loader.IsInitialized)
            {
                loader.InitLoader();
                yield return null;
            }

            ISplash splash = GetSplash();
            if (!splash.IsInitialized)
            {
                splash.InitSplash();
                yield return null;
            }

            IMain main = GetMain();
            if (main.IsInitialized)
            {
                RegisterLauncher();
            }
            else
            {
                main.OnInitializeFinish += InitWhenReady;
                main.InitMain();
                yield return null;
            }
        }

        protected virtual void InitWhenReady()
        {
            IMain main = GetMain();
            main.OnInitializeFinish -= InitWhenReady;
            RegisterLauncher();
        }
        #endregion

        #region Loading
        protected virtual void RegisterLauncher()
        {
            var loader = GetLoader();
            loader.RegisterLauncher(SceneName, this);
        }

        public virtual void Load(OnLoadFinish onFinish)
        {
            if (State == SceneLoadState.NotLoaded)
            {
                StartCoroutine(LoadingScene(onFinish));
            }
        }

        protected virtual IEnumerator LoadingScene(OnLoadFinish onFinish)
        {
            State = SceneLoadState.Loading;
            yield return InitScene();
            State = SceneLoadState.Loaded;

            if (onFinish != null)
            {
                onFinish();
            }
        }
        #endregion

        #region Unload
        public virtual void Unload(OnLoadFinish onFinish)
        {
            if (State == SceneLoadState.Loaded)
            {
                StartCoroutine(UnloadingScene(onFinish));
            }
        }

        protected virtual IEnumerator UnloadingScene(OnLoadFinish onFinish)
        {
            State = SceneLoadState.Unloading;
            yield return TerminateScene();
            State = SceneLoadState.NotLoaded;

            if (onFinish != null)
            {
                onFinish();
            }
        }
        #endregion


        #region Initialize
        protected virtual IEnumerator InitScene()
        {
            yield return InitDependencies();
            yield return InitSceneView();
            yield return InitSceneObject();
        }

        protected virtual IEnumerator InitDependencies()
        {
            _dependencies = GetSceneDependencies();

            if (_dependencies != null)
            {
                int count = _dependencies.Length;
                for (int i = 0; i < count; i++)
                {
                    yield return _dependencies[i].Initialize();
                }

                for (int i = 0; i < count; i++)
                {
                    yield return _dependencies[i].Finalize();
                }
            }
        }

        protected virtual IEnumerator InitSceneView()
        {
            // if scene is not assigned, initialize scene object here
            // otherwise do nothing
            yield return null;
        }
        #endregion

        #region Terminate
        protected virtual IEnumerator TerminateScene()
        {
            yield return RemoveDependencies();
            yield return DestroySceneView();
        }

        protected virtual IEnumerator RemoveDependencies()
        {
            if (_dependencies != null)
            {
                int count = _dependencies.Length;
                for (int i = 0; i < count; i++)
                {
                    yield return _dependencies[i].Terminate();
                }
                _dependencies = null;
            }
        }

        protected virtual IEnumerator DestroySceneView()
        {
            GameObject.Destroy(_view.gameObject);
            yield return null;
        }
        #endregion

        #region Abstract Method
        protected abstract IMain GetMain();
        protected abstract ILoad GetLoader();
        protected abstract ISplash GetSplash();
        protected abstract IController[] GetSceneDependencies();
        protected abstract IEnumerator InitSceneObject();
        #endregion
    }
}
