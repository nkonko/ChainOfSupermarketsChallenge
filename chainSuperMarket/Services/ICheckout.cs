using SuperMarket.DTO;

namespace SuperMarket.Services
{
    public interface ICheckout
    {
        Product? Scan(string itemCode);

        void InsertOnCart(Product product, int quantity);

        Invoice GetTotal();
    }
}
