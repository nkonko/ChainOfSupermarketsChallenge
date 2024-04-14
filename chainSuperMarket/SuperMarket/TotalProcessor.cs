using SuperMarket.DTO;
using System.Collections.Generic;

namespace chainSuperMarket
{
    public class TotalProcessor : ITotalProcessor
    {
        public Invoice Calculate(List<Product> products)
        {
            var invoice = new Invoice();

            foreach (var product in products)
            {
                var detail = new Detail();
                detail.Product = product;

                switch (product.Code)
                {
                    case "GR1":
                        if (product.Quantity % 2 != 0)
                        {
                            product.Quantity++;
                        }

                        detail.SubTotal = (product.Quantity / 2) * product.Price;
                        break;
                    case "SR1":
                        detail.SubTotal = product.Quantity >= 3 ? product.Quantity * 4.50m : product.Quantity * product.Price;
                        break;
                    case "CF1":
                        detail.SubTotal = product.Quantity >= 3 ? product.Quantity * ((2/3) * product.Price) : product.Quantity * product.Price;
                        break;
                    default:
                        detail.SubTotal = product.Quantity * product.Price;
                        break;
                }

                invoice.Details.Add(detail);
                invoice.Total += detail.SubTotal;
            }

            return invoice;
        }
    }
}
