using Newtonsoft.Json;
using SuperMarket.DTO;

namespace SuperMarket.Services.Database.Imp
{
    public class MockReader : IMockReader
    {
        public static ProductList? MockedData { get; set; }

        public void PopulateMockedData(string jsonPath)
        {
            try
            {
                if (jsonPath != null)
                {
                    var jsonText = File.ReadAllText(jsonPath);
                    MockedData = JsonConvert.DeserializeObject<ProductList>(jsonText);
                }
                else
                {
                    Console.WriteLine("Error: JsonFilePath not specified in appsettings.json");
                }

                if (MockedData == null)
                {
                    throw new Exception("Mock is empty please fix and restart");
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
        }
    }
}
