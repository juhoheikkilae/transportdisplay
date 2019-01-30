using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TransportDisplay.API.Logger;
using TransportDisplay.API.Services;
using TransportDisplay.API.Clients;
using TransportDisplay.API.Settings;
using TransportDisplay.API.SignalR;
using System.Net.Http;

namespace TransportDisplay.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(x => x.AddPolicy("CorsPolicy", builder =>
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200")));

            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Use HSL graph API to fetch timetable information
            services.AddSingleton<ITimetableClient>(
                s => new HslClient(new HttpClient())
            );

            services.AddSingleton<ITimetableService, TimetableService>();

            // Get OpenWeatherMap API key from secrets store
            services.AddSingleton<IWeatherClient>(
                s => new WeatherClient(
                    new HttpClient(),
                    Configuration["Weather:ApiKey"])
            );

            services.AddSingleton<IWeatherService, WeatherService>();

            services.AddSingleton<IAlertService, AlertService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");
            }
            else
            {
                app.UseHsts();
            }

            app.UseSignalR(routes => routes.MapHub<AlertHub>("/alerthub"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
