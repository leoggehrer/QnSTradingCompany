using QnSTradingCompany.Logic.Entities.Persistence.App;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Persistence.App
{
    partial class OrderController
    {
        protected override async Task BeforeInsertingUpdateingAsync(Order entity)
        {
            using var productCtrl = new MasterData.ProductController(this);
            using var conditionCtrl = new ConditionController(this);
            var conditions = await conditionCtrl.GetOrderConditionsAsync(entity.ProductId, entity.CustomerId)
                                                .ConfigureAwait(false);
            var product = await productCtrl.GetByIdAsync(entity.ProductId).ConfigureAwait(false);

            entity.Discount = 0;
            entity.Count = entity.Count >= 0 ? entity.Count : 0;
            if (product != null)
            {
                entity.PriceNet = entity.Count * product.Price;
            }

            foreach (var item in conditions)
            {
                if (item.ConditionType == Contracts.Modules.Common.ConditionType.PieceDiscountAbsolute)
                {
                    if (entity.Count >= item.Quantity)
                    {
                        decimal discount = (decimal)item.Value;
                    
                        if (discount > entity.Discount)
                        {
                            entity.Discount = discount;
                        }
                    }
                }
                else if (item.ConditionType == Contracts.Modules.Common.ConditionType.PieceDiscountRelative)
                {
                    if (entity.Count >= item.Quantity)
                    {
                        decimal discount = (entity.PriceNet * (decimal)item.Value / 100);

                        if (discount > entity.Discount)
                        {
                            entity.Discount = discount;
                        }
                    }
                }
                else if (item.ConditionType == Contracts.Modules.Common.ConditionType.ValueDiscountAbsolute)
                {
                    if (entity.PriceNet >= (decimal)item.Quantity)
                    {
                        decimal discount = (decimal)item.Value;

                        if (discount > entity.Discount)
                        {
                            entity.Discount = discount;
                        }
                    }
                }
                else if (item.ConditionType == Contracts.Modules.Common.ConditionType.ValueDiscountRelative)
                {
                    if (entity.PriceNet >= (decimal)item.Quantity)
                    {
                        decimal discount = (entity.PriceNet * (decimal)item.Value / 100);

                        if (discount > entity.Discount)
                        {
                            entity.Discount = discount;
                        }
                    }
                }
            }
            await base.BeforeInsertingUpdateingAsync(entity).ConfigureAwait(false);
        }
    }
}
