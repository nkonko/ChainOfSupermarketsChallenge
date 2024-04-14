using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SuperMarket.DTO;
using System;
using System.IO;

namespace chainSuperMarket.Database.Imp
{
    public class MockReader : IMockReader
    {
        public ProductList? PopulateMockedData(IConfiguration config)
        {
            try
            {
                var jsonPath = config["JsonFilePath"];

                if (jsonPath != null)
                {
                    var jsonText = File.ReadAllText(jsonPath);
                    return JsonConvert.DeserializeObject<ProductList>(jsonText);
                }
                else
                {
                    Console.WriteLine("Error: JsonFilePath not specified in appsettings.json");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: JSON file not found.");
            }
            catch (JsonException)
            {
                Console.WriteLine("Error: Error parsing JSON file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }
    }
}
