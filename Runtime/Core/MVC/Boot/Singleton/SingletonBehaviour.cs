using UnityEngine;

namespace Agate.MVC.Core
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        private static T _instance;
        private static readonly object padlock = new object();
        public static T Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        T t = GameObject.FindObjectOfType<T>();
                        if (t != null)
                        {
                            _instance = t;
                        }
                        else
                        {
                            GameObject obj = new GameObject(typeof(T).Name + "");
                            _instance = obj.AddComponent<T>();
                            GameObject.DontDestroyOnLoad(obj);
                        }
                    }
                    return _instance;
                }
            }
        }
    }
}
