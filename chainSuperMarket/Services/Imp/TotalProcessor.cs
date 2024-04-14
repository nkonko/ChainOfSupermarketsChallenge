using SuperMarket.DTO;
using SuperMarket.Services.Strategy;
using SuperMarket.Services.Strategy.Imp;

namespace SuperMarket.Services
{
    public class TotalProcessor : ITotalProcessor
    {
        private readonly Dictionary<string, IPriceCalculationStrategy> strategies;

        public TotalProcessor()
        {
            strategies = new Dictionary<string, IPriceCalculationStrategy>
            {
                { "GR1", new BuyOneGetOneStrategy() },
                { "SR1", new BerryPriceStrategy() },
                { "CF1", new CoffeePriceStrategy() }
            };
        }

        public Invoice Calculate(List<Product> products)
        {
            var invoice = new Invoice();

            foreach (var product in products)
            {
                var detail = new Detail();
                detail.Product = product;

                if (strategies.ContainsKey(product.Code))
                {
                    var strategy = strategies[product.Code];
                    detail.SubTotal = strategy.CalculateSubTotal(product);
                }
                else
                {
                    detail.SubTotal = product.Quantity * product.Price;
                }

                invoice.Details.Add(detail);
                invoice.Total += detail.SubTotal;
            }

            return invoice;
        }
    }
}
