using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Swashbuckle.AspNetCore.Swagger;
using RestApiCoreTrainings.Filters;
using Microsoft.Extensions.Logging;
using RestApiCoreTrainings.Authorization;

namespace RestApiCoreTrainings
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
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            services.AddMvc(options =>
            {
                options.Filters.Add(new LogExceptionFilter(loggerFactory.CreateLogger<LogExceptionFilter>()));
                options.Filters.Add(new LogAsyncExceptionFilter(loggerFactory.CreateLogger<LogAsyncExceptionFilter>()));
                options.Filters.Add(new LogActionFilter(loggerFactory.CreateLogger<LogActionFilter>()));
                options.Filters.Add(new LogAsyncActionFilter(loggerFactory.CreateLogger<LogAsyncActionFilter>()));
            });

            services.AddSingleton<IPersonService, PersonService>();

            services.AddAuthorization(options =>
                {
                    options.AddPolicy("AtLeast18", policy =>
                    {
                        policy.Requirements.Add(new MinimumAgeRequirement(18));
                    });
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
