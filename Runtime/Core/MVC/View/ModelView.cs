namespace Agate.MVC.Core
{
    public abstract class ModelView<TModel> : View
        where TModel : IModel
    {
        protected TModel _model;

        public void SetModel(TModel model)
        {
            if (_model != null)
            {
                _model.OnDataModified -= UpdateView;
            }

            if (model != null)
            {
                _model = model;
                _model.OnDataModified += UpdateView;
                InitRenderModel(_model);
                UpdateRenderModel(_model);
            }
        }

        public virtual void UpdateView()
        {
            if (_model != null)
            {
                UpdateRenderModel(_model);
            }
        }

        protected abstract void InitRenderModel(TModel model);
        protected abstract void UpdateRenderModel(TModel model);
    }
}
