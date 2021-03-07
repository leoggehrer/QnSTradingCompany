//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models
{
    public abstract partial class IdentityModel : ModelObject, Contracts.IIdentifiable
    {
        private int id;
        private int saveId;
        private bool cloneData;

        public virtual int Id 
        {
            get => id;
            set
            {
                if (cloneData)
                {
                    saveId = value;
                }
                else
                {
                    id = value;
                    saveId = 0;
                }
            }
        }
        public bool Cloneable => Id > 0 || saveId > 0;
        public bool CloneData
        {
            get { return cloneData; }
            set
            {
                if (value)
                {
                    saveId = id;
                    id = 0;
                }
                else if (saveId > 0)
                {
                    id = saveId;
                    saveId = 0;
                }
                cloneData = value;
            }
        }
    }
}
//MdEnd
