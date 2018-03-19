using Domain.Requests;
using Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetOrderByAsync(OrderRequest orderRequest, string userId);

        Task<ResponseBase> PostAsync(OrderRequest orderRequest, string userId);

        Task<ResponseBase> PutAsync(OrderRequest orderRequest);
    }
}