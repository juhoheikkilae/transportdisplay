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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<TransportDisplay.API.Logger.ILogger, DebugLogger>();
            services.AddSingleton<IArrivalEstimateClient, ArrivalEstimateClient>();
            services.AddSingleton<ITimetableClient, HslTimetableClient>();
            services.AddSingleton<ITimetableService, TimetableService>();

            // Use HSL graph API to fetch timetable information
            services.AddSingleton<ITimetableClient>(
                s => new HslTimetableClient(new HttpClient()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin());
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
