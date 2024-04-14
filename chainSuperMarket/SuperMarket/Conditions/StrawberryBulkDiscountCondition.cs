namespace chainSuperMarket.Conditions
{
    public class StrawberryBulkDiscountCondition : ISpecialCondition
    {
        public decimal CalculatePrice(int quantity, decimal price)
        {
            return quantity >= 3 ? quantity * 4.50m : quantity * price;
        }
    }
}
