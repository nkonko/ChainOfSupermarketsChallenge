using SuperMarket.DTO;

namespace SuperMarket.Services.Database.Imp
{
    public class MockDatasource : IProductDataSource
    {
        public List<Product> GetAvailableProducts()
        {
            return MockReader.MockedData!.Products!;
        }
    }
}
