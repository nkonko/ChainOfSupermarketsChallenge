using System.Collections.Generic;

namespace chainSuperMarket
{
    public class Cart
    {
        public List<Product> Products { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }
    }
}
