namespace Domain.Requests
{
    public class ItemRequest
    {
        public ProductRequest Product { get; set; }

        public decimal Quantity { get; set; }

        public decimal SetTotal() => Product.Price * Quantity;
    }
}