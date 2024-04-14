using System;
using chainSuperMarket;
using chainSuperMarket.UI;

namespace SuperMarket.UI.Imp
{
    public class UserInterface : IUserInterface
    {
        public void PromptUserMenu(ICheckout checkout)
        {
            Console.WriteLine("Please write your name:");
            var name = Console.ReadLine();
            Console.WriteLine($"Welcome {name} \n");

            Console.WriteLine("Start typing product code and confirm with enter, or type 'EXIT' to stop \n");
            var input = GetInput($"\n Empty input, please {name} type a product code or 'EXIT' to stop \n");

            do
            {
                var product = checkout.Scan(input.ToUpper());

                if (product != null && !string.IsNullOrEmpty(product.Name))
                {
                    int quantity;
                    do
                    {
                        Console.WriteLine($"You have selected: {product.Name}, Quantity?: ");
                    } while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0);

                    checkout.InsertOnCart(product, quantity);

                    Console.WriteLine("Enter new product code, or type 'EXIT' to stop \n");
                    input = GetInput($"\n Empty input, please {name} type a product code or 'EXIT' to stop \n");
                }
                else
                {
                    input = GetInput($"\n Empty input, please {name} type a product code or 'EXIT' to stop \n");
                }


            } while (input.ToUpper() != "EXIT");

            checkout.GetTotal();
        }


        private string GetInput(string message)
        {
            var input = Console.ReadLine();

            do
            {
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine(message);
                    input = Console.ReadLine();
                }

            } while (string.IsNullOrEmpty(input));

            return input;
        }
    }
}
