using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Services.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Orders
{
    [Route("orders")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService) => _orderService = orderService;

        [AllowAnonymous]
        [HttpGet("orders")]
        public async Task<IActionResult> GetAsync([FromBody]OrderRequest orderRequest)
        {
            var user = await _orderService.GetOrderByAsync(orderRequest);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("order")]
        public async Task<IActionResult> Post([FromBody]OrderRequest orderRequest)
        {
            var user = await _orderService.PostAsync(orderRequest);

            return Ok(user);
        }
    }
}