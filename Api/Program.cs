﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Api
{
    public class Program
    {
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
    }
}