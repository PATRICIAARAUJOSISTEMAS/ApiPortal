﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public partial class Startup
    {
        private void AddservicesAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth0:Domain"];
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            })
            .AddCookie()
            .AddOpenIdConnect("Auth0", options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}";

                options.ClientId = Configuration["Auth0:ClientId"];
                options.ClientSecret = Configuration["Auth0:ClientSecret"];
                options.ResponseType = "code";
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.CallbackPath = new PathString("/signin-auth0");
                options.ClaimsIssuer = "Auth0";
            });
        }
    }
}