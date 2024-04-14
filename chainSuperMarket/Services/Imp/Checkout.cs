using SuperMarket.DTO;
using SuperMarket.Services.Database.Imp;

namespace SuperMarket.Services
{
    public class Checkout : ICheckout
    {
        private List<Product> Products = new List<Product>();
        private readonly ITotalProcessor totalProcessor;

        public Checkout(ITotalProcessor totalProcessor)
        {
            this.totalProcessor = totalProcessor;
        }

        public Product? Scan(string itemCode)
        {
            var product = GetProductByCode(itemCode);

            if (product != null)
            {
                return product;
            }

            return null;
        }

        private Product? GetProductByCode(string itemCode)
        {
            var products = GetAvailableProducts();

            if (products != null)
            {
                return products.FirstOrDefault(x => x.Code == itemCode);
            }

            return null;
        }

        private List<Product>? GetAvailableProducts()
        {
            return MockReader.MockedData!.Products;
        }

        public void InsertOnCart(Product product, int quantity)
        {
            if (Products.Any() && Products.Any(x => x.Code == product.Code))
            {
                var prod = Products.FirstOrDefault(x => x.Code == product.Code);
                prod!.Quantity += quantity;
            }
            else
            {
                product.Quantity = quantity;
                Products.Add(product);
            }
        }

        public Invoice GetTotal()
        {
            return this.totalProcessor.Calculate(Products);
        }
    }
}
