using System;

namespace chainSuperMarket
{
    public class BuyOneGetOneFree : IPriceRule
    {
        public decimal CalculatePrice(int quantity, decimal price)
        {
            int freeItems = quantity / 2;
            return (quantity - freeItems) * price;
        }
    }
}
