using System;
using SuperMarket.Services;

namespace SuperMarket.UI.Imp
{
    public class UserInterface : IUserInterface
    {
        private const string ExitCommand = "EXIT";

        private readonly ICheckout checkout;
        private readonly IConsoleWrapper consoleWrapper;

        public UserInterface(ICheckout checkout, IConsoleWrapper consoleWrapper)
        {
            this.checkout = checkout;
            this.consoleWrapper = consoleWrapper;
        }

        public void PromptUserMenu()
        {
            this.consoleWrapper.WriteLine(UIResources.WriteYourName);
            var name = this.consoleWrapper.ReadLine();
            this.consoleWrapper.Clear();
            this.consoleWrapper.WriteLine(string.Format(UIResources.WelcomeMessage, name));
            this.consoleWrapper.WriteLine(string.Format(UIResources.StartMessage, ExitCommand));

            string input;
            do
            {
                input = GetNonEmptyInput(string.Format(UIResources.EmptyInputMessage, name, ExitCommand));
                if (input.ToUpper() != ExitCommand)
                {
                    ProcessUserInput(name!, input);
                }
            } while (input.ToUpper() != ExitCommand);

            DisplayInvoice();
        }

        private void ProcessUserInput(string name, string input)
        {
            var product = this.checkout.Scan(input.ToUpper());

            if (product == null)
            {
                this.consoleWrapper.WriteLine(string.Format(UIResources.WrongCode, name, ExitCommand));
            }

            if (product != null && !string.IsNullOrEmpty(product.Name))
            {
                int quantity;
                do
                {
                    this.consoleWrapper.WriteLine(string.Format(UIResources.ProductSelected, product.Name));
                } while (!int.TryParse(this.consoleWrapper.ReadLine(), out quantity) || quantity <= 0);

                this.checkout.InsertOnCart(product, quantity);
                this.consoleWrapper.Clear();
                this.consoleWrapper.WriteLine(string.Format(UIResources.ProductInserted, ExitCommand));
            }
        }

        private string GetNonEmptyInput(string message)
        {
            var input = this.consoleWrapper.ReadLine();

            do
            {
                if (string.IsNullOrEmpty(input))
                {
                    this.consoleWrapper.WriteLine(message);
                    input = this.consoleWrapper.ReadLine();
                }

            } while (string.IsNullOrEmpty(input));

            return input;
        }

        private void DisplayInvoice()
        {
            var invoice = this.checkout.GetTotal();

            this.consoleWrapper.Clear();

            foreach (var item in invoice.Details)
            {
                this.consoleWrapper.WriteLine(string.Format(UIResources.SubTotal,
                                                item.Product.Name,
                                                item.Product.Quantity,
                                                item.SubTotal));
            }

            this.consoleWrapper.WriteLine(string.Format(UIResources.Total, invoice.Total));
        }
    }
}
