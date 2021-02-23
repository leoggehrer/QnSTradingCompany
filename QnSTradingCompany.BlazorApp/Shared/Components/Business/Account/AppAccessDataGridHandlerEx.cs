//@QnSCodeCopy
using QnSTradingCompany.BlazorApp.Models.Business.Account;
using QnSTradingCompany.BlazorApp.Models.Persistence.Account;
using QnSTradingCompany.Contracts.Persistence.Account;
using Radzen.Blazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components.Business.Account
{
    public partial class AppAccessDataGridHandler
    {
        public RadzenGrid<Role> RadzenGridRole { get; set; }

        public IEnumerable<Role> RoleModels { get; set; }

        protected Contracts.Client.IAdapterAccess<IRole> RoleAccess { get; set; }

        protected override void Constructed()
        {
            RoleAccess = ModelPage.ServiceAdapter.Create<IRole>();
        }

        public override async void RowExpand(AppAccess model)
        {
            base.RowExpand(model);

            await InvokePageAsync(async () => await LoadRolesAsync(model).ConfigureAwait(false)).ConfigureAwait(false);
        }

        public async Task ChangeRoleAssignmentAsync(bool value, int id)
        {
            AppAccess model = ExpandModel;
            if (value)
            {
                var roles = await QueryAllRolesAsync().ConfigureAwait(false);
                var role = roles.SingleOrDefault(e => e.Id == id);

                if (role != null)
                {
                    model.AddManyItem(role);
                    await AdapterAccess.UpdateAsync(model).ConfigureAwait(false);
                }
            }
            else
            {
                var item = model.ManyItems.SingleOrDefault(e => e.Id == id);

                if (item != null)
                {
                    model.RemoveManyItem(item);
                    await AdapterAccess.UpdateAsync(model).ConfigureAwait(false);
                }
            }
            await LoadRolesAsync(model).ConfigureAwait(false);
        }

        private Task<IEnumerable<IRole>> QueryAllRolesAsync()
        {
            RoleAccess.SessionToken = ModelPage.AuthorizationSession.Token;

            return RoleAccess.GetAllAsync();
        }
        private async Task LoadRolesAsync(AppAccess model)
        {
            var roles = await QueryAllRolesAsync().ConfigureAwait(false);

            RoleModels = roles.Select(e =>
            {
                var role = Role.Create(e);

                role.Assigned = model.ManyItems.Any(e => e.Id == role.Id);
                return role;
            });
        }
    }
}
