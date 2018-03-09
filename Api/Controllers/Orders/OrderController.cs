using Api.Controllers.Base;
using Domain.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers.Orders
{
    [Route("orders")]
    public class OrderController : BaseController
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService) => _orderService = orderService;

        [AllowAnonymous]
        [HttpGet("orders")]
        public async Task<IActionResult> GetAsync([FromBody]OrderRequest orderRequest)
        {
            if (UserId() == null)
                return Unauthorized();
            var user = await _orderService.GetOrderByAsync(orderRequest);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("order")]
        public async Task<IActionResult> Post([FromBody]OrderRequest orderRequest)
        {
            if (UserId() == null)
                return Unauthorized();

            orderRequest.Id = UserId().ToString();
            var user = await _orderService.PostAsync(orderRequest);

            return Ok(user);
        }
    }
}