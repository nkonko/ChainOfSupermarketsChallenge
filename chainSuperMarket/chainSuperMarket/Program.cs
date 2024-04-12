using chainSuperMarket;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

public class Program
{
    static ProductList? MockedData { get; set; }

    static void Main(string[] args)
    {
        PopulateMockedData();


    }

    private static void PopulateMockedData()
    {
        var config = GetConfiguration();
        var jsonPath = config["JsonFilePath"];

        if (jsonPath != null)
        {
            var jsonText = File.ReadAllText(jsonPath);
            MockedData = JsonConvert.DeserializeObject<ProductList>(jsonText);
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