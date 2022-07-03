using System.Collections;
using Agate.MVC.Core;

namespace Agate.MVC.Base
{
    public abstract class GroupController<TController> : BaseController<TController>
    where TController : GroupController<TController>
    {
        protected IController[] _subControllers;

        public override IEnumerator Initialize()
        {
            yield return InitializeSystem();
            Context.Instance.RegisterDependencies(typeof(TController), this);
            yield return InitSubControllers();
        }

        public override IEnumerator Finalize()
        {
            InjectDependencies(this);
            yield return FinalizeSubControllers();
            yield return FinalizeSystem();
        }

        public override IEnumerator Terminate()
        {
            UnregisterDependencies();
            yield return TerminateSubControllers();
            yield return TerminateSystem();
        }

        protected virtual IEnumerator InitializeSystem()
        {
            yield return null;
        }

        protected virtual IEnumerator FinalizeSystem()
        {
            yield return null;
        }

        protected virtual IEnumerator TerminateSystem()
        {
            yield return null;
        }

        protected virtual IEnumerator InitSubControllers()
        {
            _subControllers = GetSubControllers();

            if (_subControllers != null)
            {
                int count = _subControllers.Length;
                for (int i = 0; i < count; i++)
                {
                    yield return _subControllers[i].Initialize();
                }
            }
            yield return null;
        }

        protected virtual IEnumerator FinalizeSubControllers()
        {
            if (_subControllers != null)
            {
                int count = _subControllers.Length;
                for (int i = 0; i < count; i++)
                {
                    yield return _subControllers[i].Finalize();
                }
            }
            yield return null;
        }

        protected virtual IEnumerator TerminateSubControllers()
        {
            if (_subControllers != null)
            {
                int count = _subControllers.Length;
                for (int i = 0; i < count; i++)
                {
                    yield return _subControllers[i].Terminate();
                }
            }
            yield return null;
        }

        #region Abstract
        protected abstract IController[] GetSubControllers();
        #endregion
    }
}
