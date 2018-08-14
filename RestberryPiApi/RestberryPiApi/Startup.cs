using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Redbus;
using RestberryPiApi.HostedService;
using RestberryPiApi.PinAccess;

namespace RestberryPiApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#if DEBUG
                .AddJsonFile($"appsettings.development.json", optional: true)
#endif
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();
            services.AddSingleton<ScheduledJob>();
            services.AddSingleton<SomeJob>();
            services.AddSingleton<EventBus>();
            services.AddSingleton<UnoSquarePinsService>();
            services.AddSingleton<IHostedService, RestBerryBackgroundService>(x => new RestBerryBackgroundService(x));
            services.AddOptions();
            services.Configure<List<FakePinConfiguration>>(Configuration.GetSection("FakePinConfiguration"));

            if (Configuration.GetSection("FakePinConfiguration").Exists())
            {
                services.AddSingleton<IPiPinsService, FakePinsService>();
            }
            else
            {
                services.AddSingleton<IPiPinsService, UnoSquarePinsService>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}