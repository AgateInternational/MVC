using System.Collections;

namespace Framework.Architecture.Pattern.MVC
{
    public interface IController
    {
        IEnumerator Initialize();
        IEnumerator Finalize();
        IEnumerator Terminate();
    }
}
