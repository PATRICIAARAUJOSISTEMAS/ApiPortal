using Api.Controllers.Products;
using Api.Services.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest
{
    [TestClass]
    public class ProdutoControllerTest
    {
        private readonly Mock<IProductService> _repoMock;

        public ProdutoControllerTest()
        {
            _repoMock = new Mock<IProductService>();
        }

        public ProductRequest CreateProductRequest()
        {
            return new ProductRequest()
            {
                Name = "Doritos",
                Description = "Salgadinho de milho com queijo aromatizado...",
                Price = 6.00M
            };
        }

        [TestMethod]
        public async Task GetProductTest()
        {
            var productRequest = CreateProductRequest();
            var productsFake = GetProductFake();

            var product = _repoMock.Setup(f => f.GetProductByAsync(productRequest)).Returns(Task.FromResult(productsFake));
            Assert.IsNotNull(product, "Produto invalido");

            var productController = new ProductController(_repoMock.Object);
            var actionResult = await productController.GetProduct(productRequest);
            Assert.IsNotNull(actionResult, "produto não encontradoa");

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Post()
        {
            var productRequest = CreateProductRequest();
            var baseResponse = GetResponse().FirstOrDefault();

            var product = _repoMock.Setup(f => f.PostAsync(productRequest)).Returns(Task.FromResult(baseResponse));
            Assert.IsNotNull(product, "Produto Invalido");

            var productController = new ProductController(_repoMock.Object);
            var actionResult = await productController.Post(productRequest);
            Assert.IsNotNull(actionResult, "Produto não encontrado");

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Put()
        {
            var productRequest = CreateProductRequest();
            var baseResponse = GetResponse().FirstOrDefault();

            var product = _repoMock.Setup(f => f.Put(productRequest)).Returns(baseResponse);
            Assert.IsNotNull(product, "Produto Invalido");

            var productController = new ProductController(_repoMock.Object);
            var actionResult = productController.Put(productRequest);
            Assert.IsNotNull(actionResult, "Produto não encontrado");

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        private static IEnumerable<ProductResponse> GetProductFake() => new List<ProductResponse>();

        private static IEnumerable<ResponseBase> GetResponse() => new List<ResponseBase>();
    }
}