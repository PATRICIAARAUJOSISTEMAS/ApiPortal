using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities.Orders;
using Domain.Entities.Users;
using Domain.Interfaces;
using Domain.Requests;
using Domain.Resources;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private IMapper _mapper;
        private ResponseBase _responseBase;

        public OrderService(
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _responseBase = new ResponseBase();
        }

        public async Task<ResponseBase> DeleteAsync(string id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                _responseBase.AddMessage(string.Format(Message.X0_X1_NAO_ENCONTRADO, Message.O, Message.Pedido));
                return _responseBase;
            }

            order.SetDeleted(true);
            _orderRepository.Put(order);

            return _responseBase;
        }

        public async Task<IEnumerable<OrderResponse>> GetOrderByAsync(OrderRequest orderRequest)
        {
            var order = await _orderRepository.GetAsync(p => p.UserId == orderRequest.User.Id);

            return _mapper.Map<IEnumerable<OrderResponse>>(order);
        }

        public async Task<OrderResponse> GetOrderByIdAsync(string id)
        {
            var user = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderResponse>(user);
        }

        public async Task<ResponseBase> PostAsync(OrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                _responseBase.AddMessage(Message.REQUEST_NAO_PODE_SER_VAZIO);
                return _responseBase;
            }

            var order = CreateOrder(orderRequest);
            var itens = CreateItens(orderRequest.Itens, order.Id);

            if (order.IsFailure || itens.Any(f => f.IsFailure))
            {
                foreach (var item in itens.Select(f => f.Errors))
                    _responseBase.AddMessages(mensages: item);

                _responseBase.AddMessages(order.Errors);

                return _responseBase;
            }

            await _orderRepository.PostAsync(order);
            return _responseBase;
        }

        public async Task<ResponseBase> PutAsync(OrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                _responseBase.AddMessage(Message.REQUEST_NAO_PODE_SER_VAZIO);
                return _responseBase;
            }

            var order = await _orderRepository.GetByIdAsync(orderRequest.Id);
            var itens = CreateItens(orderRequest.Itens, order.Id);

            if (order.IsFailure || itens.Any(f => f.IsFailure))
            {
                foreach (var item in itens.Select(f => f.Errors))
                    _responseBase.AddMessages(mensages: item);

                _responseBase.AddMessages(order.Errors);

                return _responseBase;
            }

            _orderRepository.Put(order);
            return _responseBase;
        }

        private ICollection<ItemOrder> CreateItens(ICollection<ItemRequest> itensRequest, string Id)
        {
            var itemOrder = new List<ItemOrder>();

            foreach (var i in itensRequest)
                itemOrder.Add(new ItemOrder(Id, i.Product.Id, i.Quantity));

            return itemOrder;
        }

        private Order CreateOrder(OrderRequest orderRequest)
        {
            var user = _mapper.Map<User>(orderRequest.User);
            var order = new Order(Guid.NewGuid().ToString(), user.Id);
            return order;
        }
    }
}