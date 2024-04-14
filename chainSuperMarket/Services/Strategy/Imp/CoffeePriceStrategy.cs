using SuperMarket.DTO;

namespace SuperMarket.Services.Strategy.Imp
{
    public class CoffeePriceStrategy : IPriceCalculationStrategy
    {
        public decimal CalculateSubTotal(Product product)
        {
            return product.Quantity >= 3 ? Math.Round(product.Quantity * ((2m / 3) * product.Price), 2) : product.Quantity * product.Price;
        }
    }
}
