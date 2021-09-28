using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Agate.MVC.Core
{
    public abstract class Loader<T> : Singleton<T>, ILoad where T : Loader<T>, new() 
    {
        public delegate void SceneLoadEvent(string sceneName);

        public event SceneLoadEvent OnSceneChanged;

        public string PreviousScene { get { return _previousScene; } }
        public string CurrentScene { get { return _currentScene; } }
        public string RequestedScene { get { return _requestedScene; } }

        protected Dictionary<string, ILauncher> _sceneLaunchers = new Dictionary<string, ILauncher>();
        protected string _currentScene = null;
        protected string _previousScene = null;
        protected string _requestedScene = null;

        #region Load
        public void LoadScene(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                UnityEngine.Debug.Log("Load Scene " + sceneName);
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

        public void RequestLoadScene(string sceneName, ILauncher sceneController)
        {
            UnityEngine.Debug.Log("Request Load Scene " + sceneName);
            if (!_sceneLaunchers.ContainsKey(sceneName))
            {
                _sceneLaunchers.Add(sceneName, sceneController);
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
            ShowLoadingView();

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
            HideLoadingView();
        }
        #endregion

        #region Unload
        protected virtual void StartUnload()
        {
            UnityEngine.Debug.Log("Start Unload " + _currentScene);
            ShowLoadingView();

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
            StartLoad();
        }
        #endregion

        #region Restart Scene
        public void RestartScene()
        {
            UnityEngine.Debug.Log("Restart Scene " + _currentScene);
            ShowLoadingView();
            _sceneLaunchers[_currentScene].Unload(ReloadScene);
        }

        protected virtual void ReloadScene()
        {
            UnityEngine.Debug.Log("Reload Scene " + _currentScene);
            _requestedScene = _currentScene;
            StartLoad();
        }
        #endregion

        #region Loading View
        protected virtual void ShowLoadingView()
        {

        }

        protected virtual void HideLoadingView()
        {

        }
        #endregion
    }
}