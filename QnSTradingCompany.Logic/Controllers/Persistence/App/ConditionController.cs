using Microsoft.EntityFrameworkCore;
using QnSTradingCompany.Logic.Entities.Persistence.App;
using System.Linq;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Persistence.App
{
    partial class ConditionController
    {
        public Task<Condition[]> GetOrderConditionsAsync(int productId, int customerId)
        {
            return QueryableSet().Where(c => c.ProductId == productId && c.CustomerId == customerId)
                                 .Include(c => c.Product)
                                 .Include(c => c.Customer)
                                 .ToArrayAsync();
        }
    }
}
