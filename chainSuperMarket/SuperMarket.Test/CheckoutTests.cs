using Moq;
using SuperMarket.DTO;
using SuperMarket.Services.Database;
using SuperMarket.Services;
using Xunit;
using FluentAssertions;
using SuperMarket.Services.Strategy;

namespace SuperMarket.Test
{
    public class CheckoutTests
    {
        [Fact]
        public void Scan_ProductExists_ReturnsProduct()
        {
            var strategies = new Dictionary<string, IPriceCalculationStrategy>();
            var itemCode = "A";
            var mockProductDataSource = new Mock<IProductDataSource>();
            mockProductDataSource.Setup(x => x.GetAvailableProducts()).Returns(new List<Product>
            {
                new Product { Code = "A", Price = 10 }
            });
            var checkout = new Checkout(new TotalProcessor(strategies), mockProductDataSource.Object);

            var product = checkout.Scan(itemCode);

            product.Should().NotBeNull();
            product!.Code.Should().Be(itemCode);
        }

        [Fact]
        public void Scan_ProductDoesNotExist_ReturnsNull()
        {
            var strategies = new Dictionary<string, IPriceCalculationStrategy>();
            var itemCode = "X";
            var mockProductDataSource = new Mock<IProductDataSource>();
            mockProductDataSource.Setup(x => x.GetAvailableProducts()).Returns(new List<Product>());
            var checkout = new Checkout(new TotalProcessor(strategies), mockProductDataSource.Object);

            var product = checkout.Scan(itemCode);

            product.Should().BeNull();
        }

        [Fact]
        public void InsertOnCart_AddNewProduct_AddsToCart()
        {
            var strategies = new Dictionary<string, IPriceCalculationStrategy>();
            var product = new Product { Code = "A", Price = 10 };
            var quantity = 2;
            var checkout = new Checkout(new TotalProcessor(strategies), Mock.Of<IProductDataSource>());

            checkout.InsertOnCart(product, quantity);

            checkout.SelectedProducts.Should().ContainSingle(p => p.Code == product.Code && p.Quantity == quantity);
        }

        [Fact]
        public void InsertOnCart_AddExistingProductWithAdditionalQuantity_UpdatesQuantity()
        {
            var strategies = new Dictionary<string, IPriceCalculationStrategy>();
            var product = new Product { Code = "A", Price = 10 };
            var initialQuantity = 2;
            var additionalQuantity = 3;
            var checkout = new Checkout(new TotalProcessor(strategies), Mock.Of<IProductDataSource>());

            checkout.InsertOnCart(product, initialQuantity);
            checkout.InsertOnCart(product, additionalQuantity);

            var selectedProduct = checkout.SelectedProducts.Single(p => p.Code == product.Code);
            selectedProduct.Quantity.Should().Be(initialQuantity + additionalQuantity);
        }

        [Fact]
        public void GetTotal_WithProductsInCart_CalculatesTotal()
        {
            var products = new List<Product>
            {
                new Product { Code = "A", Price = 10, Quantity = 2 },
                new Product { Code = "B", Price = 20, Quantity = 1 }
            };

            var totalProcessorMock = new Mock<ITotalProcessor>();
            var expectedInvoice = new Invoice { Total = 40 }; 
            totalProcessorMock.Setup(x => x.Calculate(products)).Returns(expectedInvoice);
            var checkout = new Checkout(totalProcessorMock.Object, Mock.Of<IProductDataSource>());

            foreach (var product in products)
            {
                checkout.InsertOnCart(product, product.Quantity);
            }

            var total = checkout.GetTotal();

            total.Should().BeEquivalentTo(expectedInvoice);
        }

        [Fact]
        public void GetTotal_WithoutProductsInCart_ReturnsZeroTotal()
        {
            var totalProcessorMock = new Mock<ITotalProcessor>();
            var checkout = new Checkout(totalProcessorMock.Object, Mock.Of<IProductDataSource>());

            var total = checkout.GetTotal();

            total.Should().BeNull();
        }

    }
}
