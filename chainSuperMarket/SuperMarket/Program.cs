using SuperMarket.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Services;
using SuperMarket.Services.Database;
using SuperMarket.Services.Database.Imp;
using SuperMarket.UI.Imp;
using System.IO;
using SuperMarket.Services.Strategy.Imp;
using SuperMarket.Services.Strategy;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        var strategies = new Dictionary<string, IPriceCalculationStrategy>
                            {
                                { "GR1", new BuyOneGetOneStrategy() },
                                { "SR1", new BerryPriceStrategy() },
                                { "CF1", new CoffeePriceStrategy() }
                            };

        var serviceProvider = new ServiceCollection()
            .AddTransient<IConsoleWrapper, ConsoleWrapper>()
            .AddTransient<IProductDataSource, MockDatasource>()
            .AddTransient<IMockReader, MockReader>()
            .AddTransient<IUserInterface, UserInterface>()
            .AddTransient<ICheckout, Checkout>()
            .AddTransient<ITotalProcessor>(provider => new TotalProcessor(strategies))
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