using SuperMarket.DTO;
using System.Collections.Generic;

namespace chainSuperMarket
{
    public interface ITotalProcessor
    {
        Invoice Calculate(List<Product> products);
    }
}