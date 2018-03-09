using Api.Controllers.Base;
using Domain.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers.Products
{
    [Route("products")]
    public class ProductController : BaseController
    {
        private IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<IActionResult> GetProduct([FromBody]ProductRequest productRequest)
        {
            if (UserId() == null)
                return Unauthorized();
            var product = await _productService.GetProductByAsync(productRequest);

            return Ok(product);
        }

        [AllowAnonymous]
        [HttpPost("product")]
        public async Task<IActionResult> Post([FromBody]ProductRequest productRequest)
        {
            if (UserId() == null)
                return Unauthorized();
            var user = await _productService.PostAsync(productRequest);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPut("product")]
        public IActionResult Put([FromBody]ProductRequest productRequest)
        {
            if (UserId() == null)
                return Unauthorized();
            var user = _productService.Put(productRequest);

            return Ok(user);
        }
    }
}