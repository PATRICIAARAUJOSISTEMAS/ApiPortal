using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Services.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Products
{
    [Route("products")]
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<IActionResult> GetProduct([FromBody]ProductRequest productRequest)
        {
            var product = await _productService.GetProductByAsync(productRequest);

            return Ok(product);
        }

        [AllowAnonymous]
        [HttpPost("product")]
        public async Task<IActionResult> Post([FromBody]ProductRequest productRequest)
        {
            var user = await _productService.PostAsync(productRequest);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPut("product")]
        public IActionResult Put([FromBody]ProductRequest productRequest)
        {
            var user = _productService.Put(productRequest);

            return Ok(user);
        }
    }
}