namespace SuperMarket.DTO
{
    public class Invoice
    {
        public List<Detail> Details { get; set; } = new List<Detail>();

        public decimal Total { get; set; }
    }
}
