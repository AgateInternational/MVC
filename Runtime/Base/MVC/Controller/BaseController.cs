using System;
using System.Collections;
using Agate.MVC.Core;

namespace Agate.MVC.Base
{
    public class BaseController<TController> : Controller
        where TController : BaseController<TController>
    {
        public override IEnumerator Initialize()
        {
            yield return RegisterDependencies();
        }

        public override IEnumerator Finalize()
        {
            yield return InjectDependencies();
        }

        public override IEnumerator Terminate()
        {
            UnregisterDependencies();
            yield return null;
        }

        #region DI
        protected virtual IEnumerator RegisterDependencies()
        {
            Context.Instance.RegisterDependencies(typeof(TController), this);
            yield return null;
        }

        protected virtual IEnumerator InjectDependencies()
        {
            InjectDependencies(this);
            yield return null;
        }

        protected virtual void InjectDependencies(object obj)
        {
            Context.Instance.InjectDependencies(obj);
        }

        protected virtual void UnregisterDependencies()
        {
            UnregisterDependencies(typeof(TController));
        }

        protected virtual void UnregisterDependencies(Type type)
        {
            Context.Instance.UnregisterDependencies(type);
        }
        #endregion

        #region PubSub
        protected virtual void Publish<TMessage>(TMessage message) where TMessage : struct
        {
            Context.Instance.Publish(message);
        }
        #endregion
    }
}
