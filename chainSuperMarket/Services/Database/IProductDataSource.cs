using SuperMarket.DTO;

namespace SuperMarket.Services.Database
{
    public interface IProductDataSource
    {
        List<Product> GetAvailableProducts();
    }
}
