using Domain.Requests;
using Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProductByAsync(ProductRequest productRequest);

        Task<ResponseBase> PostAsync(ProductRequest productRequest);

        ResponseBase Put(ProductRequest productRequest);
    }
}