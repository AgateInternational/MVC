using System.Collections;

namespace Agate.MVC.Base
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

    public abstract class ObjectController<TController, TModel, TView> : BaseController<TController>
        where TController : ObjectController<TController, TModel, TView>
        where TModel : BaseModel, new()
        where TView : BaseView
    {
        protected TView _view;
        protected TModel _model;

        public ObjectController()
        {
            _model = new TModel();
        }

        public virtual void SetView(TView view)
        {
            _view = view;
        }
    }


    public abstract class ObjectController<TController, TModel, TInterfaceModel, TView> : BaseController<TController>
        where TController : ObjectController<TController, TModel, TInterfaceModel, TView>
        where TModel : BaseModel, TInterfaceModel, new()
        where TInterfaceModel : IBaseModel
        where TView : ObjectView<TInterfaceModel>
    {
        protected TView _view;
        protected TModel _model;

        public TInterfaceModel Model
        {
            get { return _model; }
        }

        public ObjectController()
        {
            _model = new TModel();
        }

        public virtual void SetView(TView view)
        {
            _view = view;
            _view.SetModel(_model);
        }
    }
}