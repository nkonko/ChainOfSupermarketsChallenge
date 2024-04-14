namespace chainSuperMarket.Conditions
{
    public interface ISpecialCondition
    {
        decimal CalculatePrice(int quantity, decimal price);
    }
}
