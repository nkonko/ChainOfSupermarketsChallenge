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

            mockCheckout.Verify(c => c.GetTotal(), Times.Once);
            mockConsole.Verify(c => c.WriteLine("Please write your name:"), Times.Once);
        }

        //[Fact]
        //public void ProcessUserInput_ProductNotFound_WritesErrorMessage()
        //{
        //    var mockCheckout = new Mock<ICheckout>();
        //    mockCheckout.Setup(c => c.Scan(It.IsAny<string>())).Returns<Product>(null); 

        //    var ui = new UserInterface(mockCheckout.Object, m);
        //    ui.PromptUserMenu();
        //}
    }
}
