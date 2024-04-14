using SuperMarket.DTO;
using SuperMarket.Services.Strategy;

namespace SuperMarket.Services
{
    public class TotalProcessor : ITotalProcessor
    {
        private readonly Dictionary<string, IPriceCalculationStrategy> strategies;

        public TotalProcessor(Dictionary<string, IPriceCalculationStrategy> strategies)
        {
            this.strategies = strategies;
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
