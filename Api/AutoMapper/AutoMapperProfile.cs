using Auth0.AuthenticationApi.Models;
using AutoMapper;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Entities.Users;
using Domain.Requests;
using Domain.Responses;

namespace Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapUser();
            MapProduct();
            MapOrder();
            MapItemOrder();
        }

        private void MapItemOrder()
        {
            CreateMap<ItemOrder, ItemOrder>();
            CreateMap<OrderResponse, ItemOrder>();

            CreateMap<ItemOrder, ItemRequest>();
            CreateMap<ItemRequest, ItemOrder>();
        }

        private void MapOrder()
        {
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderResponse, Order>();

            CreateMap<Order, OrderRequest>();
            CreateMap<OrderRequest, Order>();
        }

        private void MapProduct()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductResponse, Product>();

            CreateMap<Product, ProductRequest>();
            CreateMap<ProductRequest, Product>();
        }

        private void MapUser()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>();

            CreateMap<User, UserRequest>();
            CreateMap<UserRequest, User>();

            CreateMap<User, UserInfo>();
            CreateMap<UserInfo, User>();

            CreateMap<UserRequest, UserInfo>();
            CreateMap<UserInfo, UserRequest>();

            CreateMap<UserResponse, UserInfo>();
            CreateMap<UserInfo, UserResponse>();
        }
    }
}