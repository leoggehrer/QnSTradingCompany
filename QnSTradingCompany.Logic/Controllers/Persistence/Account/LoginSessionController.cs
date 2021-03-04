//@QnSCodeCopy
//MdStart

using Microsoft.EntityFrameworkCore;
using QnSTradingCompany.Logic.Entities.Persistence.Account;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Persistence.Account
{
	partial class LoginSessionController
	{
		protected override Task BeforeInsertingAsync(LoginSession entity)
		{
			entity.LoginTime = DateTime.Now;
			entity.LastAccess = entity.LoginTime;
			entity.SessionToken = Guid.NewGuid().ToString();
			return base.BeforeInsertingAsync(entity);
		}

		public Task<LoginSession[]> QueryOpenLoginSessionsAsync()
		{
			return QueryableSet().Where(e => e.LogoutTime.HasValue == false)
								 .Include(e => e.Identity)
								 .ToArrayAsync();
		}
	}
}
//MdEnd
