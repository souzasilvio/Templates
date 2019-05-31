using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel;

using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace WebApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
           CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>();

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //WebHost.CreateDefaultBuilder(args)       
        //.UseStartup<Startup>()
        //.ConfigureKestrel((context, options) =>
        //{
        //    options.Listen(IPAddress.Loopback, 5000);
        //    options.Listen(IPAddress.Loopback, 443, listenOptions =>
        //    {
        //        listenOptions.UseHttps("myCertificate.pfx", "Davisamuel@123");
        //    });
        //});
    }
}
