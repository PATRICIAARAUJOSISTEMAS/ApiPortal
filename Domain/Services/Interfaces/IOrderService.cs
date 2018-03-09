using Domain.Requests;
using Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetOrderByAsync(OrderRequest orderRequest);

        Task<ResponseBase> PostAsync(OrderRequest orderRequest);

        Task<ResponseBase> PutAsync(OrderRequest orderRequest);
    }
}