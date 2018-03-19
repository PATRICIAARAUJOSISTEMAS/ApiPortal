using Api.Controllers.Base;
using Domain.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers.Orders
{
    // [Authorize]
    [Route("orders")]
    public class OrderController : BaseController
    {
        private IOrderService _orderService;
        private IUserService _userService;

        public OrderController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAsync([FromQuery]OrderRequest orderRequest)
        {
            var userIdGuid = UserId().ToString();
            var user = _userService.GetByIdAsync(UserId().ToString());
            var order = await _orderService.GetOrderByAsync(orderRequest, userIdGuid);

            return Ok(order);
        }

        [HttpPost("order")]
        public async Task<IActionResult> Post([FromBody]OrderRequest orderRequest)
        {
            var userIdGuid = UserId().ToString();
            orderRequest.Id = UserId().ToString();
            var order = await _orderService.PostAsync(orderRequest, userIdGuid);

            return Ok(order);
        }
    }
}