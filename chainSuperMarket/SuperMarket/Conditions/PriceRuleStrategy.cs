namespace chainSuperMarket.Conditions
{
    public class SpecialConditionFactory
    {
        public static ISpecialCondition CreateSpecialCondition(string productCode)
        {
            switch (productCode)
            {
                case "GR1":
                    return new BuyOneGetOneFreeCondition();
                case "SR1":
                    return new StrawberryBulkDiscountCondition();
                case "CF1":
                    return new CoffeeBulkDiscountCondition();
                default:
                    return null;
            }
        }
    }
}
