using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Agate.MVC.Core
{
    public abstract class Loader<T> : SingletonBehaviour<T>, ILoad where T : Loader<T>
    {
        public event SceneLoadEvent OnSceneChanged;
        public event TransitionEvent OnStartTransition;
        public event TransitionEvent OnFinishTransition;

        public string PreviousScene { get { return _previousScene; } }
        public string CurrentScene { get { return _currentScene; } }
        public string RequestedScene { get { return _requestedScene; } }

        public bool IsInitialized { get; protected set; } = false;

        protected Dictionary<string, ILauncher> _sceneLaunchers = new Dictionary<string, ILauncher>();
        protected string _currentScene = null;
        protected string _previousScene = null;
        protected string _requestedScene = null;

        #region Init
        public void InitLoader()
        {
            if(!IsInitialized){
                // load splash scene
                SceneManager.LoadScene(SplashScene, LoadSceneMode.Additive);
                IsInitialized = true;
            }
        }
        #endregion


        #region Load
        public void LoadScene(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                UnityEngine.Debug.Log("Load Scene " + sceneName);
                _requestedScene = sceneName;

                if (string.IsNullOrEmpty(_currentScene))
                {
                    // if current scene is fully unloaded
                    SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                }
                else
                {
                    // otherwise unload current scene first
                    StartUnload();
                }
            }
        }

        public void RegisterLauncher(string sceneName, ILauncher sceneLauncher)
        {
            UnityEngine.Debug.Log("Register Scene Launcher" + sceneName);
            if (!_sceneLaunchers.ContainsKey(sceneName))
            {
                _sceneLaunchers.Add(sceneName, sceneLauncher);
            }

            _requestedScene = sceneName;

            if (string.IsNullOrEmpty(_currentScene))
            {
                StartLoad();
            }
            else
            {
                StartUnload();
            }
        }

        protected virtual void StartLoad()
        {
            UnityEngine.Debug.Log("Start Load " + _requestedScene);
            StartTransition();

            if (_requestedScene != _currentScene)
            {
                if (OnSceneChanged != null)
                {
                    OnSceneChanged(_requestedScene);
                }
            }
            _sceneLaunchers[_requestedScene].Load(FinishLoad);
        }

        protected virtual void FinishLoad()
        {
            UnityEngine.Debug.Log("Finish Load " + _requestedScene);
            _currentScene = _requestedScene;
            _requestedScene = null;
            FinishTransition();
        }
        #endregion

        #region Unload
        protected virtual void StartUnload()
        {
            UnityEngine.Debug.Log("Start Unload " + _currentScene);
            StartTransition();

            if (!string.IsNullOrEmpty(_currentScene))
            {
                _sceneLaunchers[_currentScene].Unload(UnloadScene);
                _sceneLaunchers.Remove(_currentScene);
            }
        }

        protected virtual void UnloadScene()
        {
            UnityEngine.Debug.Log("Unload Scene " + _currentScene);
            var async = SceneManager.UnloadSceneAsync(_currentScene, UnloadSceneOptions.None);

            if (_previousScene != _currentScene)
            {
                _previousScene = _currentScene;
            }
            _currentScene = null;
            async.completed += (a) => { FinishUnload(); };
        }

        protected virtual void FinishUnload()
        {
            UnityEngine.Debug.Log("Finish Unload");
            LoadScene(_requestedScene);
        }
        #endregion

        #region Restart Scene
        public void RestartScene()
        {
            UnityEngine.Debug.Log("Restart Scene " + _currentScene);
            StartTransition();
            _requestedScene = _currentScene;
            StartUnload();
        }
        #endregion

        #region Broadcast Event
        protected virtual void StartTransition()
        {
            if (OnStartTransition != null)
            {
                OnStartTransition();
            }
        }

        protected virtual void FinishTransition()
        {
            if (OnFinishTransition != null)
            {
                OnFinishTransition();
            }
        }
        #endregion

        #region Abstract Method
        protected abstract string SplashScene { get; }
        #endregion
    }
}