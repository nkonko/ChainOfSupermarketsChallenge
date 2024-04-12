using System.Collections.Generic;

namespace chainSuperMarket
{
    public class Checkout
    {
        private List<Product> cart;

        public void Scan(string itemCode)
        {
            var product = GetProductByCode(itemCode);

            cart.Add(product);
        }

        public decimal Total()
        {
            var total = 0m;

            foreach (var item in cart)
            {

            }

            return total;
        }

        private Product? GetProductByCode(string itemCode)
        {
            var products = GetAvailableProducts();

            if (products != null)
            {
                var product =  products.Find(x => x.Code == itemCode);

                return product ?? null;
            }

            return null;
        }

        public List<Product> GetAvailableProducts()
        {
            return new List<Product>();
        }
    }
}
