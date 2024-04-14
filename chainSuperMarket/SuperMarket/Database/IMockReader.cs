using Microsoft.Extensions.Configuration;
using SuperMarket.DTO;

namespace chainSuperMarket.Database
{
    public interface IMockReader
    {
        ProductList? PopulateMockedData(IConfiguration config);
    }
}