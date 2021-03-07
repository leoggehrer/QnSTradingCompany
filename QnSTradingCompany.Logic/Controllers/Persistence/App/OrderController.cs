using CommonBase.Extensions;
using QnSTradingCompany.Contracts.Persistence.MasterData;
using QnSTradingCompany.Logic.Entities.Persistence.App;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnSTradingCompany.Logic.Controllers.Persistence.App
{
    partial class OrderController
    {
        protected override async Task BeforeInsertingUpdateingAsync(Order entity)
        {
            using var productCtrl = new MasterData.ProductController(this);
            using var conditionCtrl = new ConditionController(this);
            var product = await productCtrl.GetByIdAsync(entity.ProductId).ConfigureAwait(false);
            var conditions = await conditionCtrl.GetOrderConditionsAsync(entity.ProductId, entity.CustomerId)
                                                .ConfigureAwait(false);

            entity.Discount = 0;
            entity.Count = entity.Count >= 0 ? entity.Count : 0;
            if (product != null)
            {
                entity.PriceNet = entity.Count * product.Price;
                entity.Discount = CalculateMaxCondition(entity, product, conditions);
            }

            await base.BeforeInsertingUpdateingAsync(entity).ConfigureAwait(false);
        }

        public static decimal CalculateMaxCondition(Order entity, IProduct product, IEnumerable<Condition> conditions)
        {
            entity.CheckArgument(nameof(entity));
            product.CheckArgument(nameof(product));
            conditions.CheckArgument(nameof(conditions));

            decimal result = 0;
            decimal priceNet = entity.Count * product.Price;

            foreach (var item in conditions)
            {
                if (item.ConditionType == Contracts.Modules.Common.ConditionType.PieceDiscountAbsolute)
                {
                    if (entity.Count >= item.Quantity)
                    {
                        decimal discount = item.Value;

                        if (discount > result)
                        {
                            result = discount;
                        }
                    }
                }
                else if (item.ConditionType == Contracts.Modules.Common.ConditionType.PieceDiscountRelative)
                {
                    if (entity.Count >= item.Quantity)
                    {
                        decimal discount = (priceNet * (decimal)item.Value / 100);

                        if (discount > result)
                        {
                            result = discount;
                        }
                    }
                }
                else if (item.ConditionType == Contracts.Modules.Common.ConditionType.ValueDiscountAbsolute)
                {
                    if (priceNet >= (decimal)item.Quantity)
                    {
                        decimal discount = item.Value;

                        if (discount > result)
                        {
                            result = discount;
                        }
                    }
                }
                else if (item.ConditionType == Contracts.Modules.Common.ConditionType.ValueDiscountRelative)
                {
                    if (priceNet >= (decimal)item.Quantity)
                    {
                        decimal discount = (priceNet * item.Value / 100);

                        if (discount > result)
                        {
                            result = discount;
                        }
                    }
                }
            }
            return result;
        }
    }
}
