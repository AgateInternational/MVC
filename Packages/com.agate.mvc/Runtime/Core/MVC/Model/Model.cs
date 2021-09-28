namespace Agate.MVC.Core
{
    public delegate void OnDataModified();

    public class Model
    {
        public event OnDataModified OnDataModified;

        protected void SetDataAsDirty()
        {
            if (OnDataModified != null)
            {
                OnDataModified();
            }
        }
    }
}
