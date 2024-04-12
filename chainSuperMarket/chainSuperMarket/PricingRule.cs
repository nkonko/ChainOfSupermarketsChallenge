namespace chainSuperMarket
{
    public class PricingRule
    {
        public DiscountType DiscountType { get; set; }

        public int ProductId { get; set; }

        public int Threshold { get; set; }
    }
}
