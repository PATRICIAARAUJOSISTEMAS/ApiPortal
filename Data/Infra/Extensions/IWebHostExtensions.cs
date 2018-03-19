using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.Infra.Extensions
{
    public static class IWebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<TContext>>();

                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Migrando base {typeof(TContext).Name}");

                    //  context.Database.Migrate();
                    seeder(context, services);

                    logger.LogInformation($"Base migrada {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Um erro ocorreu {typeof(TContext).Name}");
                }
            }

            return webHost;
        }
    }
}