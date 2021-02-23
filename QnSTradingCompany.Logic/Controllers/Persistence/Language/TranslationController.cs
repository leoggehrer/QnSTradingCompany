//@QnSCodeCopy
//MdStart

using QnSTradingCompany.Contracts.Persistence.Language;
using QnSTradingCompany.Logic.Modules.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Persistence.Language
{
	partial class TranslationController
    {
        [AllowAnonymous]
        public override Task<IEnumerable<ITranslation>> QueryAllAsync(string predicate)
        {
            return ExecuteQueryAllAsync(predicate);
        }
    }
}
//MdEnd
