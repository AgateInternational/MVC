using System;
using System.Collections.Generic;
using System.Reflection;

namespace Agate.MVC.Core
{
    public class DependencyInjection
    {
        private static DependencyInjection _instance;
        public static DependencyInjection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DependencyInjection();
                }
                return _instance;
            }
        }

        protected Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

        protected DependencyInjection() { }

        #region Register Dependencies
        public bool RegisterDependencies<T>() where T : new()
        {
            Type objectType = typeof(T);
            if (!_dependencies.ContainsKey(objectType))
            {
                T t = new T();
                _dependencies.Add(typeof(T), t);
                return true;
            }
            return false;
        }

        public bool RegisterDependencies<T>(T t)
        {
            return RegisterDependencies(typeof(T), t);
        }

        public bool RegisterDependencies(Type type, object t)
        {
            if (!_dependencies.ContainsKey(type))
            {
                _dependencies.Add(type, t);
                return true;
            }
            return false;
        }
        #endregion

        #region Unregister
        public bool UnregisterDependencies<T>()
        {
            Type objectType = typeof(T);
            return UnregisterDependencies(objectType);
        }

        public bool UnregisterDependencies(Type type)
        {
            if (_dependencies.ContainsKey(type))
            {
                _dependencies.Remove(type);
                return true;
            }
            return false;
        }
        #endregion


        #region Inject Dependencies
        public void InjectDependencies(object target)
        {
            InjectProperty(target);
            InjectField(target);
        }

        private void InjectProperty(object target)
        {
            PropertyInfo[] props = target.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (var prop in props)
            {
                if (prop.PropertyType.IsClass || prop.PropertyType.IsInterface)
                {
                    object source;
                    if (_dependencies.TryGetValue(prop.PropertyType, out source))
                    {
                        prop.SetValue(target, source);
                    }
                }
            }
        }

        private void InjectField(object target)
        {
            FieldInfo[] fields = target.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (var field in fields)
            {
                if (field.FieldType.IsClass || field.FieldType.IsInterface)
                {
                    object source;
                    if (_dependencies.TryGetValue(field.FieldType, out source))
                    {
                        field.SetValue(target, source);
                    }
                }
            }
        }
        #endregion
    }
}
