using Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Requests
{
    public class OrderRequest
    {
        public string Id { get; set; }
        public ICollection<ItemRequest> Itens { get; set; }
        public UserRequest User { get; set; }
    }
}