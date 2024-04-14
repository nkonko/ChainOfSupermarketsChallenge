using SuperMarket.DTO;

namespace SuperMarket.Services.Strategy.Imp
{
    public class BuyOneGetOneStrategy : IPriceCalculationStrategy
    {
        public decimal CalculateSubTotal(Product product)
        {
            if (product.Quantity % 2 != 0)
            {
                product.Quantity++;
            }

            return (product.Quantity / 2) * product.Price;
        }
    }
}
