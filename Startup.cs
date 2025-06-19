#pragma warning disable CA1416
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RutasApi.Data;
using RutasApi.Services;
using System.Runtime.InteropServices;
using RutasApi.Interfaces;

namespace RutasApi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        private readonly string MyLogName = "SICEM";
        private readonly string MySourceName = "UBITOMA";
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();
            services.AddDbContext<SicemContext>( options => options.UseSqlServer(Configuration.GetConnectionString("Sicem")!));
            services.AddScoped<SicemService>();
            services.AddScoped<ImagenesService>();
            services.AddScoped<ArquosService>();
            services.AddScoped<ICatalogService, ArquosCatalogService>();
            services.AddScoped<OperadorService>();
            services.Configure<UbitomaSettings>(Configuration.GetSection("UbitomaSettings"));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RutasApi", Version = "v1" });
            });
            services.AddLogging(options => {
                if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
                    options.AddEventLog(config => {
                        config.LogName = MyLogName;
                        config.SourceName = MySourceName;
                    });
                }
            });

            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX",false);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-MX",false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RutasApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
