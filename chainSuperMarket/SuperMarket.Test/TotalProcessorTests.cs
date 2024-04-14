using FluentAssertions;
using Moq;
using SuperMarket.DTO;
using SuperMarket.Services;
using SuperMarket.Services.Strategy;
using SuperMarket.Services.Strategy.Imp;
using Xunit;

namespace SuperMarket.Test
{
    public class TotalProcessorTests
    {
        [Fact]
        public void Calculate_Should_Return_Correct_Total_Without_Strategies()
        {
            var products = new List<Product>
            {
                new Product { Code = "GR1", Quantity = 2, Price = 3.11m },
                new Product { Code = "SR1", Quantity = 1, Price = 5.00m },
                new Product { Code = "CF1", Quantity = 3, Price = 11.23m }
            };

            var strategies = new Dictionary<string, IPriceCalculationStrategy>();
            var totalProcessor = new TotalProcessor(strategies);

            var result = totalProcessor.Calculate(products);

            result.Should().NotBeNull();
            result.Total.Should().Be(44.91m);
        }

        [Fact]
        public void Calculate_Should_Return_Correct_Total_With_Strategies()
        {
            var products = new List<Product>
            {
                new Product { Code = "GR1", Quantity = 2, Price = 3.11m },
                new Product { Code = "SR1", Quantity = 1, Price = 5.00m },
                new Product { Code = "CF1", Quantity = 3, Price = 11.23m }
            };

            var mockBuyOneGetOneStrategy = new Mock<IPriceCalculationStrategy>();
            mockBuyOneGetOneStrategy.Setup(x => x.CalculateSubTotal(It.IsAny<Product>())).Returns(6.22m);

            var mockBerryPriceStrategy = new Mock<IPriceCalculationStrategy>();
            mockBerryPriceStrategy.Setup(x => x.CalculateSubTotal(It.IsAny<Product>())).Returns(4.50m);

            var mockCoffeePriceStrategy = new Mock<IPriceCalculationStrategy>();
            mockCoffeePriceStrategy.Setup(x => x.CalculateSubTotal(It.IsAny<Product>())).Returns(33.69m);

            var strategies = new Dictionary<string, IPriceCalculationStrategy>
            {
                { "GR1", mockBuyOneGetOneStrategy.Object },
                { "SR1", mockBerryPriceStrategy.Object },
                { "CF1", mockCoffeePriceStrategy.Object }
            };

            var totalProcessor = new TotalProcessor(strategies);

            var result = totalProcessor.Calculate(products);

            result.Should().NotBeNull();
            result.Total.Should().Be(44.41m);
        }

        [Fact]
        public void Calculate_Should_Return_Correct_Total_With_Missing_Strategy()
        {
            var products = new List<Product>
            {
                new Product { Code = "GR1", Quantity = 2, Price = 3.11m },
                new Product { Code = "SR1", Quantity = 1, Price = 5.00m },
                new Product { Code = "CF1", Quantity = 3, Price = 11.23m }
            };

            var mockBuyOneGetOneStrategy = new Mock<IPriceCalculationStrategy>();
            mockBuyOneGetOneStrategy.Setup(x => x.CalculateSubTotal(It.IsAny<Product>())).Returns(6.22m);

            var strategies = new Dictionary<string, IPriceCalculationStrategy>
            {
                { "GR1", mockBuyOneGetOneStrategy.Object },
                { "CF1", new CoffeePriceStrategy() } 
            };

            var totalProcessor = new TotalProcessor(strategies);

            var result = totalProcessor.Calculate(products);

            result.Should().NotBeNull();
            result.Total.Should().Be(33.68m);
        }
    }
}
