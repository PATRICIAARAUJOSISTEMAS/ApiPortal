using Data.Context;
using Domain.Entities.Orders;
using Domain.Interfaces;

namespace Data.Infra.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context)
           : base(context) => _context = context;
    }
}