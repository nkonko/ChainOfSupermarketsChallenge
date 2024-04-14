using SuperMarket.DTO;

namespace chainSuperMarket
{
    public interface ICheckout
    {
        Product? Scan(string itemCode);

        void InsertOnCart(Product product, int quantity);

        void GetTotal();
    }
}
