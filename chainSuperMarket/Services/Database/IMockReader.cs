namespace SuperMarket.Services.Database
{
    public interface IMockReader
    {
        void PopulateMockedData(string jsonPath);
    }
}