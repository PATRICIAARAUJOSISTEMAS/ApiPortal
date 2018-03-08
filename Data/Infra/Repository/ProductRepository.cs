using Data.Context;
using Domain.Entities.Products;
using Domain.Interfaces;

namespace Data.Infra.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DataContext context)
           : base(context) => _context = context;
    }
}