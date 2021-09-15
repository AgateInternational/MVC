namespace Framework.Architecture.Pattern.MVC
{
    public class Singleton<T> where T : class, new()
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}
