using Api.Services.Base;
using Domain.Requests;
using Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IOrderService : IScopedServiceBase
    {
        Task<IEnumerable<OrderResponse>> GetOrderByAsync(OrderRequest orderRequest);

        Task<ResponseBase> PostAsync(OrderRequest orderRequest);

        Task<ResponseBase> PutAsync(OrderRequest orderRequest);
    }
}