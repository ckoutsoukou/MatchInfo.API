using MatchInfo.API.Controllers;
using MatchInfo.API.DbContexts;
using MatchInfo.API.Filters;
using MatchInfo.API.GlobalErrorHandling.Extensions;
using MatchInfo.API.Repositories;
using MatchInfo.API.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MatchInfo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public class ApplicationLogger
        {
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("MatchInfoOPENAPISpecification", new OpenApiInfo
                {
                    Version = "1",
                    Title = "MatchInfo API",
                    Description = "A rest web api for matches.",
                    Contact = new OpenApiContact()
                    { 
                        Name = "Xristina Koutsoukou",
                        Email = "xristina.koytsoykoy@gmail.com"
                    }
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                c.IncludeXmlComments(xmlCommentsFullPath);
            });

           

            //JSON Serializer
            services.AddControllersWithViews(
                
                ).AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());

            services.AddControllers(config =>
            {
            });

            services.AddScoped<ValidationFilterAttribute>();

            services.Configure<ApiBehaviorOptions>(opts => opts.SuppressModelStateInvalidFilter = true);

            services.AddDbContext<MatchInfoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MatchInfoDbContext")));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddLogging(configure => configure.SetMinimumLevel(LogLevel.Information));


            // Additional code to register the ILogger as a ILogger<T> where T is the Startup class
            services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));
            services.AddScoped<UnitOfWork.UnitOfWorkMatchInfo>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/MatchInfoOPENAPISpecification/swagger.json", "MatchInfo API");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }    

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
