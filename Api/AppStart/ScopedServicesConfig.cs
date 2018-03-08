using Api.Services.Interfaces;
using Data.Infra.Repository;
using Domain.Interfaces;
using Domain.Orders;
using Domain.Products;
using Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Api.AppStart
{
    public static class ScopedServicesConfig
    {
        public static void ConfigureScopedServices(this IServiceCollection services)
        {
            ResolveServices(services);
            ResolveRepository(services);
        }

        private static void ResolveRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
        }

        private static void ResolveServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
        }
    }
}