using System.Collections;
using Agate.MVC.Core;

namespace Agate.MVC.Base
{
    public abstract class BaseLoader<T> : Loader<T> where T : BaseLoader<T>, new()
    {
        
    }
}
