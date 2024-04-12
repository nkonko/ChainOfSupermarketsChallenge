using System;

namespace chainSuperMarket
{
    public class BulkDiscountRule : IPriceRule
    {
        public decimal CalculatePrice(int quantity, decimal price)
        {
            if (quantity >= 3)
            {
                return quantity * 4.50m;
            }
            return quantity * price;
        }
    }
}
