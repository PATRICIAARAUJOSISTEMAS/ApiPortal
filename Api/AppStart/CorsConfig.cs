using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Api.AppStart
{
    public static class CorsConfig
    {
        //TODO: Montar logica de WithOrigins para ambiente de dev x prod
        public static void ConfigureCors(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(x => x
                //.WithOrigins("http://example.com")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        }
    }
}