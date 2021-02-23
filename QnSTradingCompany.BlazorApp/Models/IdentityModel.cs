//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models
{
    public abstract partial class IdentityModel : ModelObject, Contracts.IIdentifiable
    {
        private int _id;
        public virtual int Id
        {
            get
            {
                OnIdReading();
                return _id;
            }
            set
            {
                bool handled = false;
                OnIdChanging(ref handled, ref _id);
                if (handled == false)
                {
                    this._id = value;
                }
                OnIdChanged();
            }
        }
        partial void OnIdReading();
        partial void OnIdChanging(ref bool handled, ref int _id);
        partial void OnIdChanged();
    }
}
//MdEnd
