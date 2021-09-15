using System.Collections;

namespace Framework.Architecture.Base
{
    public interface IConnector
    {
        IEnumerator Init();
        IEnumerator Terminate();
    }
}