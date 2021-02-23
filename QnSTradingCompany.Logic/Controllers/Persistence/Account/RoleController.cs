//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using QnSTradingCompany.Logic.Entities.Persistence.Account;
using System.Text;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Persistence.Account
{
    partial class RoleController
    {
        protected override Task BeforeInsertingUpdateingAsync(Role entity)
        {
            entity.Designation = ClearRoleDesignation(entity.Designation);

            return base.BeforeInsertingUpdateingAsync(entity);
        }
        public static string ClearRoleDesignation(string name)
        {
            StringBuilder result = new StringBuilder();

            if (name.HasContent())
            {
                foreach (var item in name)
                {
                    if (char.IsLetter(item) || char.IsDigit(item))
                    {
                        result.Append(result.Length == 0 ? char.ToUpper(item) : item);
                    }
                }
            }
            return result.Length > 0 ? result.ToString() : null;
        }
    }
}
//MdEnd
