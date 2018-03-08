using Api.Services.Base;
using Domain.Requests;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IProductService : IScopedServiceBase
    {
        Task<IEnumerable<ProductResponse>> GetProductByAsync(ProductRequest productRequest);

        Task<ResponseBase> PostAsync(ProductRequest productRequest);

        ResponseBase Put(ProductRequest productRequest);
    }
}