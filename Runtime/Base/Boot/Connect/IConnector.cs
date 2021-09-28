using System.Collections;

namespace Agate.MVC.Base
{
    public interface IConnector
    {
        IEnumerator Init();
        IEnumerator Terminate();
    }
}