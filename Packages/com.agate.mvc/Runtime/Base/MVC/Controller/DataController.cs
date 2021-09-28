namespace Agate.MVC.Base
{
    public abstract class DataController<TController, TModel> : BaseController<TController>
        where TController : DataController<TController, TModel>
        where TModel : BaseModel, new()
    {
        protected TModel _model;

        public DataController()
        {
            _model = new TModel();
        }
    }

    public abstract class DataController<TController, TModel, TInterfaceModel> : DataController<TController, TModel>
        where TController : DataController<TController, TModel, TInterfaceModel>
        where TModel : BaseModel, TInterfaceModel, new()
        where TInterfaceModel : IBaseModel
    {
        public TInterfaceModel Model
        {
            get { return _model; }
        }
    }
}
