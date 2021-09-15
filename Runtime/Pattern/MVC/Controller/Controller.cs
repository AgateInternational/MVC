using System.Collections;

namespace Framework.Architecture.Pattern.MVC
{
    public abstract class Controller : IController
    {
        public virtual IEnumerator Initialize()
        {
            yield return null;
        }

        public virtual IEnumerator Finalize()
        {
            yield return null;
        }

        public virtual IEnumerator Terminate()
        {
            yield return null;
        }
    }
}
