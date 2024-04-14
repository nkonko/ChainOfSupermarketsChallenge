namespace chainSuperMarket.Conditions
{
    public class BuyOneGetOneFreeCondition : ISpecialCondition
    {
        public decimal CalculatePrice(int quantity, decimal price)
        {
            return quantity / 2 * price + quantity % 2 * price;
        }
    }
}
