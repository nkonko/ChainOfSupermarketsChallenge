using SuperMarket.DTO;

namespace SuperMarket.Services.Strategy
{
    public interface IPriceCalculationStrategy
    {
        decimal CalculateSubTotal(Product product);
    }
}
