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
        public abstract string SceneName {get; }
        #endregion

        [SerializeField]
        protected TView _view;
        protected IController[] _dependencies;

        protected void Awake()
        {
            AutoInitialize();
        }

        #region Auto Initialize
        protected virtual void AutoInitialize()
        {
            var main = GetMain();
            if (main.IsInitialized)
            {
                RequestLoad();
            }
            else
            {
                main.OnInitialized += InitWhenReady;
            }
        }

        protected virtual void InitWhenReady()
        {
            var main = GetMain();
            main.OnInitialized -= InitWhenReady;
            RequestLoad();
        }
        #endregion

        #region Loading
        protected virtual void RequestLoad()
        {
            var loader = GetLoader();
            loader.RequestLoadScene(SceneName, this);
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
        protected abstract IController[] GetSceneDependencies();
        protected abstract IEnumerator InitSceneObject();
        #endregion
    }
}
