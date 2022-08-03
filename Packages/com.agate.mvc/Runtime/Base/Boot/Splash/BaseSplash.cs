using System.Collections;
using Agate.MVC.Core;

namespace Agate.MVC.Base
{
    public abstract class BaseSplash<T> : Splash<T>, ISplash where T : BaseSplash<T>
    {

    }
}
