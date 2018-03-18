using Api.AppStart;
using Api.Settings;
using AutoMapper;
using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace Api
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            ConfigureLoggerFactory(loggerFactory);
            app.ConfigureCors(env);
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMvc();

            app.UseSwagger()
            .UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //Error swagger not found
            app.UseStaticFiles();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("TestDb"));
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });

                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
            services.AddAutoMapper();
            AddservicesAuthentication(services);
            services.ConfigureScopedServices();
            AddServicesSwagger(services);
            services.Configure<AppSettings>(Configuration.GetSection("Auth0"));
            services.AddMvc();
        }
    }
}