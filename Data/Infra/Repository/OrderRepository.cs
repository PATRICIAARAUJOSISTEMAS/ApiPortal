using Data.Context;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Interfaces;
using Domain.Requests;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infra.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context)
           : base(context) => _context = context;
    }
}