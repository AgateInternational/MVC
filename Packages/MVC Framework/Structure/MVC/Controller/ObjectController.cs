using System.Collections;

namespace Framework.Architecture.Base
{
    public abstract class ObjectController<TController, TView> : BaseController<TController>
        where TController : ObjectController<TController, TView>
        where TView : BaseView
    {
        protected TView _view;

        public virtual void SetView(TView view)
        {
            _view = view;
        }
    }


    public abstract class ObjectController<TController, TModel, TInterfaceModel, TView> : ObjectController<TController, TView>
        where TController : ObjectController<TController, TModel, TInterfaceModel, TView>
        where TModel : BaseModel, TInterfaceModel, new()
        where TInterfaceModel : IBaseModel
        where TView : BaseModelView<TInterfaceModel>
    {
        protected TModel _model;

        public TInterfaceModel Model
        {
            get { return _model; }
        }

        public ViewController()
        {
            _model = new TModel();
        }

        public override void SetView(TView view)
        {
            _view = view;
            _view.SetModel(_model);
        }
    }
}