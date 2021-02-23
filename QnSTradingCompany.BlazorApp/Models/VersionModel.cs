//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models
{
    public abstract partial class VersionModel : IdentityModel, Contracts.IVersionable
    {
        private byte[] _rowVersion;
        public virtual byte[] RowVersion
        {
            get
            {
                OnRowVersionReading();
                return _rowVersion;
            }
            set
            {
                bool handled = false;
                OnRowVersionChanging(ref handled, ref _rowVersion);
                if (handled == false)
                {
                    this._rowVersion = value;
                }
                OnRowVersionChanged();
            }
        }
        partial void OnRowVersionReading();
        partial void OnRowVersionChanging(ref bool handled, ref byte[] _rowVersion);
        partial void OnRowVersionChanged();
    }
}
//MdEnd
