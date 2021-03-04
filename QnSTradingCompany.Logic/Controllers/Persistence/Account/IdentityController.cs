//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using QnSTradingCompany.Logic.Entities.Persistence.Account;
using QnSTradingCompany.Logic.Modules.Account;
using QnSTradingCompany.Logic.Modules.Exception;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Persistence.Account
{
	partial class IdentityController
    {
        private static void CheckInsertEntity(Identity entity)
        {
            if (AccountManager.CheckMailAddressSyntax(entity.Email) == false)
            {
                throw new LogicException(ErrorType.InvalidEmail);
            }
            if (AccountManager.CheckPasswordSyntax(entity.Password) == false)
            {
                throw new LogicException(ErrorType.InvalidPassword);
            }
        }
        private static void CheckUpdateEntity(Identity entity)
        {
            if (AccountManager.CheckMailAddressSyntax(entity.Email) == false)
            {
                throw new LogicException(ErrorType.InvalidEmail);
            }
            if (entity.Password.HasContent())
            {
                if (AccountManager.CheckPasswordSyntax(entity.Password) == false)
                {
                    throw new LogicException(ErrorType.InvalidPassword);
                }
            }
        }

        protected override Task BeforeInsertingAsync(Identity entity)
        {
            CheckInsertEntity(entity);

            var (Hash, Salt) = AccountManager.CreatePasswordHash(entity.Password);

            entity.PasswordHash = Hash;
            entity.PasswordSalt = Salt;
            entity.Guid = System.Guid.NewGuid().ToString();

            return base.BeforeInsertingAsync(entity);
        }
        protected override Task BeforeUpdatingAsync(Identity entity)
        {
            CheckUpdateEntity(entity);
            if (entity.Password.HasContent())
            {
                var (Hash, Salt) = AccountManager.CreatePasswordHash(entity.Password);

                entity.PasswordHash = Hash;
                entity.PasswordSalt = Salt;
            }
            return base.BeforeUpdatingAsync(entity);
        }

        public Task<Identity> GetValidIdentityByEmail(string email)
		{
            return QueryableSet().Include(e => e.IdentityXRoles)
                                 .ThenInclude(e => e.Role)
                                 .FirstOrDefaultAsync(e => e.State == Contracts.Modules.Common.State.Active
                                                        && e.AccessFailedCount < 4
                                                        && e.Email.ToLower() == email.ToLower());
		}
    }
}
//MdEnd
