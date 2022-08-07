using System;
using System.Collections;
using Agate.MVC.Core;

namespace Agate.MVC.Base
{
    public abstract class BaseLauncher<TController, TView> : Launcher<TView>
        where TController : BaseLauncher<TController, TView>
        where TView : View
    {
        protected IConnector[] _connectors;

        #region Initialize
        protected override IEnumerator InitScene()
        {
            yield return InitDependencies();
            yield return InitConnector();
            yield return InitSceneView();
            yield return InitSceneObject();
            yield return LaunchScene();
        }

        protected override IEnumerator InitDependencies()
        {
            Context.Instance.RegisterDependencies(typeof(TController), this);
            yield return base.InitDependencies();
            Context.Instance.InjectDependencies(this);
        }

        protected virtual IEnumerator InitConnector()
        {
            _connectors = GetSceneConnectors();

            if (_connectors != null)
            {
                int count = _connectors.Length;
                for (int i = 0; i < count; i++)
                {
                    yield return _connectors[i].Init();
                }
            }
        }
        #endregion

        #region Terminate
        protected override IEnumerator TerminateScene()
        {
            yield return base.TerminateScene();
            yield return TerminateConnector();
        }

        protected override IEnumerator RemoveDependencies()
        {
            yield return base.RemoveDependencies();
            Context.Instance.UnregisterDependencies(typeof(TController));
        }

        protected virtual IEnumerator TerminateConnector()
        {
            if (_connectors != null)
            {
                int count = _connectors.Length;
                for (int i = 0; i < count; i++)
                {
                    yield return _connectors[i].Terminate();
                }
                _connectors = null;
            }
        }
        #endregion

        #region Game Pattern
        protected virtual void Publish<TMessage>(TMessage message) where TMessage : struct
        {
            Context.Instance.Publish(message);
        }
        #endregion

        #region Abstract
        protected abstract IConnector[] GetSceneConnectors();
        protected abstract IEnumerator LaunchScene();
        #endregion
    }
}
