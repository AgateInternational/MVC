using System.Collections;
using Framework.Architecture.Pattern.MVC;

namespace Framework.Architecture.Base
{
    public abstract class BaseMain<T> : Main<T> where T : BaseMain<T>, IMain
    {
        protected IConnector[] _connectors;

        protected override IEnumerator FinalizeInit()
        {
            yield return InitConnector();
        }

        protected virtual IEnumerator InitConnector()
        {
            _connectors = GetSystemConnectors();

            if (_connectors != null)
            {
                int count = _connectors.Length;
                for (int i = 0; i < count; i++)
                {
                    yield return _connectors[i].Init();
                }
            }
        }

        #region Abstract
        protected abstract IConnector[] GetSystemConnectors();
        #endregion
    }
}
