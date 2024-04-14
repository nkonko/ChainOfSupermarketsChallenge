using SuperMarket.DTO;

namespace SuperMarket.Services.Strategy.Imp
{
    public class BerryPriceStrategy : IPriceCalculationStrategy
    {
        public decimal CalculateSubTotal(Product product)
        {
            return product.Quantity >= 3 ? product.Quantity * 4.50m : product.Quantity * product.Price;
        }
    }
}
