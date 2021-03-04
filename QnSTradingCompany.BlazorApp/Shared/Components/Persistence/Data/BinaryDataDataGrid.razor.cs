//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.Data.IBinaryData;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Data.BinaryData;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Data
{
    partial class BinaryDataDataGrid
    {
        [Parameter]
        public BinaryDataDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "BinaryData";
    }
}
