namespace Domain.Requests
{
    public class ProductRequest
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}