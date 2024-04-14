using SuperMarket.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Services;
using SuperMarket.Services.Database;
using SuperMarket.Services.Database.Imp;
using SuperMarket.UI.Imp;
using System.IO;

public class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<ICheckout, Checkout>()
            .AddTransient<IConsoleWrapper, ConsoleWrapper>()
            .AddTransient<IUserInterface, UserInterface>()
            .AddTransient<ITotalProcessor, TotalProcessor>()
            .AddTransient<IMockReader, MockReader>()
            .BuildServiceProvider();

        var config = GetConfiguration();
        var mockReader = serviceProvider.GetRequiredService<IMockReader>();

        mockReader.PopulateMockedData(config["JsonFilePath"]!);

        var uinterface = serviceProvider.GetRequiredService<IUserInterface>();

        uinterface.PromptUserMenu();
    }

    private static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
    }
}