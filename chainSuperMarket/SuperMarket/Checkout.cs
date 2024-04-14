using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chainSuperMarket
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
                var product = products.FirstOrDefault(x => x.Code == itemCode);

                if (product == null)
                {
                    Console.WriteLine("Invalid code or product not available try with other code \n");
                }

                return product;
            }

            throw new Exception("Database is empty");
        }

        private List<Product>? GetAvailableProducts()
        {
            return Program.MockedData!.Products;
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

        public void GetTotal()
        {
            Console.Clear();

            var invoice = this.totalProcessor.Calculate(Products);

            foreach (var item in invoice.Details)
            {
                Console.WriteLine($"Product: {item.Product.Name}, Quantity: {item.Product.Quantity}, Sub Total: {item.SubTotal}");
            }

            Console.WriteLine($"\n Total: {invoice.Total}");
        }
    }
}
