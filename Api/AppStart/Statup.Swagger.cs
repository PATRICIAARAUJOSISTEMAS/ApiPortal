using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
{
    public partial class Startup
    {
        private void AddServicesSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Order API",
                    Version = "v1",
                    Description = "Order HTTP API",
                });
            });
            services.AddMvc();
        }
    }
}