using SuperMarket.DTO;
using SuperMarket.Services.Database;

namespace SuperMarket.Services
{
    public class Checkout : ICheckout
    {
        private List<Product> Products = new List<Product>();
        private List<Product> ProductsSelected = new List<Product>();
        private readonly ITotalProcessor totalProcessor;
        private readonly IProductDataSource productDataSource;

        public Checkout(ITotalProcessor totalProcessor, IProductDataSource productDataSource)
        {
            this.totalProcessor = totalProcessor;
            this.productDataSource = productDataSource;
        }

        public Product? Scan(string itemCode)
        {
            if (!Products.Any())
            {
                Products = productDataSource.GetAvailableProducts();
            }

            var product = Products.FirstOrDefault(x => x.Code == itemCode);

            if (product != null)
            {
                return product;
            }

            return null;
        }

        public void InsertOnCart(Product product, int quantity)
        {
            if (ProductsSelected.Any() && ProductsSelected.Any(x => x.Code == product.Code))
            {
                var prod = ProductsSelected.FirstOrDefault(x => x.Code == product.Code);
                prod!.Quantity += quantity;
            }
            else
            {
                product.Quantity = quantity;
                ProductsSelected.Add(product);
            }
        }

        public Invoice GetTotal()
        {
            return this.totalProcessor.Calculate(ProductsSelected);
        }
    }
}
