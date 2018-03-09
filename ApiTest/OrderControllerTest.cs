using Api.Controllers.Orders;
using Domain.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest
{
    [TestClass]
    public class OrderControllerTest
    {
        private readonly Mock<IOrderService> _repoMock;

        public OrderControllerTest()
        {
            _repoMock = new Mock<IOrderService>();
        }

        [TestMethod]
        public async Task GetOrderTest()
        {
            var orderRequest = CreateOrderRequest();
            var OrderFake = GetOrderFake();

            var product = _repoMock.Setup(f => f.GetOrderByAsync(orderRequest)).Returns(Task.FromResult(OrderFake));
            Assert.IsNotNull(product, "Pedido invalido");

            var productController = new OrderController(_repoMock.Object);
            var actionResult = await productController.GetAsync(orderRequest);
            Assert.IsNotNull(actionResult, "Pedido não encontradoa");

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Post()
        {
            var productRequest = CreateOrderRequest();
            var baseResponse = GetResponse().FirstOrDefault();

            var product = _repoMock.Setup(f => f.PostAsync(productRequest)).Returns(Task.FromResult(baseResponse));
            Assert.IsNotNull(product, "Produto Invalido");

            var productController = new OrderController(_repoMock.Object);
            var actionResult = await productController.Post(productRequest);
            Assert.IsNotNull(actionResult, "Produto não encontrado");

            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult));
        }

        private static IEnumerable<OrderResponse> GetOrderFake() => new List<OrderResponse>();

        private static IEnumerable<ResponseBase> GetResponse() => new List<ResponseBase>();

        private OrderRequest CreateOrderRequest()
        {
            return new OrderRequest()
            {
                Itens = new List<ItemRequest>
            {
                new ItemRequest(){
                    Product = new ProductRequest()
                    {
                        Name = "Doritos",
                        Description = "Salgadinho de milho com queijo aromatizado...",
                        Price = 6.00M
                    },
                    Quantity = 2.00M,
                }
            },
                User = null
            };
        }
    }
}