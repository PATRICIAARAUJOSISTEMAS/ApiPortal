using Domain.Entities.Products;
using Domain.Interfaces;

namespace Domain.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
    }
}