namespace chainSuperMarket.Conditions
{
    partial class CoffeeBulkDiscountCondition : ISpecialCondition
    {
        public decimal CalculatePrice(int quantity, decimal price)
        {
            return quantity >= 3 ? quantity * (2.0m / 3.0m) * price : quantity * price;
        }
    }
}
