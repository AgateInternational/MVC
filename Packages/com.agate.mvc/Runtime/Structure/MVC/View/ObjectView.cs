using Framework.Architecture.Pattern.MVC;

namespace Framework.Architecture.Base
{
    public abstract class ObjectView<TModel> : ModelView<TModel>
    where TModel : IBaseModel
    {
    }
}
