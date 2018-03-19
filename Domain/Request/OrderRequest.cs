using System.Collections.Generic;

namespace Domain.Requests
{
    public class OrderRequest
    {
        public string Id { get; set; }
        public ICollection<ItemRequest> Itens { get; set; }
    }
}