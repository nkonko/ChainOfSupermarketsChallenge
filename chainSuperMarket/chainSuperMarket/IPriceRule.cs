namespace chainSuperMarket
{
    public interface IPriceRule
    {
        decimal CalculatePrice(int quantity, decimal price);
    }
}
