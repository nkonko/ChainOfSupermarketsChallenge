using chainSuperMarket;
using chainSuperMarket.Conditions;
using chainSuperMarket.Database;
using chainSuperMarket.Database.Imp;
using chainSuperMarket.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.DTO;
using SuperMarket.UI.Imp;
using System;
using System.IO;

public class Program
{
    public static ProductList? MockedData { get; set; }

    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<ICheckout, Checkout>()
            .AddTransient<IUserInterface, UserInterface>()
            .AddTransient<ITotalProcessor, TotalProcessor>()
            .AddTransient<IMockReader, MockReader>()
            .BuildServiceProvider();

        var config = GetConfiguration();
        var checkout = serviceProvider.GetRequiredService<ICheckout>();
        var uinterface = serviceProvider.GetRequiredService<IUserInterface>();
        var mockReader = serviceProvider.GetRequiredService<IMockReader>();

        var mock = mockReader.PopulateMockedData(config);

        if (mock != null)
        {
            MockedData = mock;
            uinterface.PromptUserMenu(checkout);
        }
        else
        {
            Console.WriteLine("Mock is empty please fix and restart");
            throw new Exception("Mock is empty");
        }
    }

    private static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
    }
}