using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace RutasApi {
    public class Program {
        public static void Main(string[] args) {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX",false);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX",false);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.ConfigureKestrel(options => {
                        options.ConfigureHttpsDefaults(h => {
                            h.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
