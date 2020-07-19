using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TransactionData.Data;
using TransactionData.IoC;
using TransactionData.WebAPI.Converters;

namespace TransactionData.WebAPI
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
            services.ConfigureServices(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transaction API", Version = "v1" });
            });
            services.AddControllers()
                .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.DateParseHandling = DateParseHandling.None;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new MultiFormatDateConverter
                {
                    DateTimeFormats = new List<string>()
                    {
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ssK",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.f",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fK",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ff",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ffK",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffK",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ffff",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ffffK",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffff",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffK",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ffffff",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ffffffK",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff",
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile("Logs/TransactionDataLog-{Date}.txt", LogLevel.Error);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
