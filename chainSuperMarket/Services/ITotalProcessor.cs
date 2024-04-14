using SuperMarket.DTO;

namespace SuperMarket.Services
{
    public interface ITotalProcessor
    {
        Invoice Calculate(List<Product> products);
    }
}