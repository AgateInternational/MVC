using Agate.MVC.Core;

namespace Agate.MVC.Base
{
    public abstract class ObjectView<TModel> : ModelView<TModel>
    where TModel : IBaseModel
    {
    }
}
