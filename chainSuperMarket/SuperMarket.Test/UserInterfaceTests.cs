using Moq;
using SuperMarket.DTO;
using SuperMarket.Services;
using SuperMarket.UI;
using SuperMarket.UI.Imp;
using Xunit;

namespace SuperMarket.Test
{
    public class UserInterfaceTests
    {
        [Fact]
        public void PromptUserMenu_ExitCommand_CallsDisplayInvoice()
        {
            var mockConsole = new Mock<IConsoleWrapper>();
            mockConsole.SetupSequence(c => c.ReadLine())
                       .Returns("John")
                       .Returns("EXIT");

            var mockCheckout = new Mock<ICheckout>();
            mockCheckout.Setup(c => c.GetTotal()).Returns(new Invoice());

            var ui = new UserInterface(mockCheckout.Object, mockConsole.Object);

            ui.PromptUserMenu();

            mockConsole.Verify(c => c.WriteLine("Please write your name:"), Times.Once);
            mockCheckout.Verify(c => c.GetTotal(), Times.Once);
        }

        [Fact]
        public void ProcessUserInput_ValidInput_Should_InsertProductIntoCart()
        {
            var productName = "Apple";
            var productCode = "A001";
            var mockCheckout = new Mock<ICheckout>();
            var mockConsole = new Mock<IConsoleWrapper>();

            mockConsole.SetupSequence(c => c.ReadLine())
                       .Returns("John")
                       .Returns(productCode)
                       .Returns("1")
                       .Returns("EXIT");

            var userInterface = new UserInterface(mockCheckout.Object, mockConsole.Object);

            var product = new Product { Code = productCode, Name = productName };
            mockCheckout.Setup(c => c.Scan(productCode.ToUpper())).Returns(product);
            mockCheckout.Setup(c => c.GetTotal()).Returns(new Invoice());

            userInterface.PromptUserMenu();

            mockCheckout.Verify(c => c.InsertOnCart(It.IsAny<Product>(), 1), Times.Once);
        }

        [Fact]
        public void EmptyInput_Should_Display_A_Message()
        {
            var mockCheckout = new Mock<ICheckout>();
            var mockConsole = new Mock<IConsoleWrapper>();
            mockConsole.SetupSequence(c => c.ReadLine())
                       .Returns("John")
                       .Returns("")
                       .Returns("EXIT");

            var userInterface = new UserInterface(mockCheckout.Object, mockConsole.Object);

            userInterface.PromptUserMenu();

            mockConsole.Verify(c => c.ReadLine(), Times.Exactly(3));
            mockConsole.Verify(c => c.WriteLine("Empty input, please John type a product code or 'EXIT' to stop \n"), Times.Once);
        }

        [Fact]
        public void DisplayInvoice_Should_ShowCorrectSummary()
        {
            var mockConsole = new Mock<IConsoleWrapper>();
            mockConsole.SetupSequence(c => c.ReadLine())
                       .Returns("John")
                       .Returns("A1")
                       .Returns("2")
                       .Returns("B1")
                       .Returns("1")
                       .Returns("EXIT");

            var mockCheckout = new Mock<ICheckout>();

            var mockProd1 = new Product { Code = "A1", Name = "Apple", Quantity = 2, Price = 10.0m };
            var mockProd2 = new Product { Code = "B1", Name = "Banana", Quantity = 1, Price = 5.0m };
            var invoiceDetails = new List<Detail>
            {
                new Detail() {
                    Product = mockProd1,
                    SubTotal = 20.0m
                     },
                new Detail()
                {
                    Product = mockProd2,
                    SubTotal = 5.0m
                }
            };

            var invoiceTotal = 25.0m;

            var invoice = new Invoice
            {
                Details = invoiceDetails,
                Total = invoiceTotal
            };

            mockCheckout.Setup(c => c.GetTotal()).Returns(invoice);
            mockCheckout.SetupSequence(c => c.Scan(It.IsAny<string>())).Returns(mockProd1).Returns(mockProd2);

            var userInterface = new UserInterface(mockCheckout.Object, mockConsole.Object);

            userInterface.PromptUserMenu();

            mockConsole.Verify(c => c.Clear(), Times.Exactly(4));
            mockConsole.Verify(c => c.WriteLine("Product: Apple, Quantity: 2, Sub Total: 20.0"), Times.Once);
            mockConsole.Verify(c => c.WriteLine("Product: Banana, Quantity: 1, Sub Total: 5.0"), Times.Once);
            mockConsole.Verify(c => c.WriteLine($"\n Total: {invoiceTotal}"), Times.Once);
        }
    }
}
