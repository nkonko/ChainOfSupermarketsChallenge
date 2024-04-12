using System.Collections.Generic;

namespace chainSuperMarket
{
    public class DiscountCalculator
    {
        //public Money Calculate(List<string> basket, PricingRules pricingRules)
        //{
        //    if (pricingRules.DiscountType == DiscountType.BuyOneGetOneFree && pricingRules.ProductCodes.Any() && pricingRules.Threshold > 0)
        //    {
        //        return basket
        //            .FilterGroupAndCount(pricingRules.ProductCodes)
        //            .Aggregate(new Money(0), (discount, item) =>
        //            {
        //                var (product, qty) = item;
        //                var productPrice = Catalog.GetProduct(product).Price;
        //                var discountAmount = Money.Multiply(productPrice, qty / pricingRules.Threshold) * pricingRules.Operand;
        //                return Money.Add(discount, discountAmount);
        //            });
        //    }
        //    else if (pricingRules.DiscountType == DiscountType.BulkPurchase && pricingRules.ProductCodes.Any())
        //    {
        //        return basket
        //            .FilterGroupAndCount(pricingRules.ProductCodes)
        //            .Aggregate(new Money(0), (accDiscount, item) =>
        //            {
        //                var (product, qty) = item;
        //                if (qty >= pricingRules.Threshold)
        //                {
        //                    var productPrice = Catalog.GetProduct(product).Price;
        //                    var discountAmount = Money.Multiply(productPrice, qty) * pricingRules.Operand;
        //                    return Money.Add(accDiscount, discountAmount);
        //                }
        //                else
        //                {
        //                    return accDiscount;
        //                }
        //            });
        //    }
        //    else
        //    {
        //        return new Money(0);
        //    }
        //}
    }
}
