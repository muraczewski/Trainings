using Amazon.S3;
using Amazon.SQS;
using AwsWrappers.Interfaces;
using AwsWrappers.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Swashbuckle.AspNetCore.Swagger;
using RestApiCoreTrainings.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using RestApiCoreTrainings.Middlewares;

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
            services.AddMvc(options =>
            {
                options.Filters.Add<LogExceptionFilter>();
                options.Filters.Add<LogActionFilter>();
            });

            services.AddSingleton<IPersonService, PersonService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSQS>();
            services.AddAWSService<IAmazonS3>();
            services.AddSingleton<ISQSProvider>(s => new SQSProvider("https://sqs.eu-central-1.amazonaws.com/890769921003/gmuraczewski-queue", "gmuraczewski-queue"));
            services.AddScoped<ISQSClientWrapper, SQSClientWrapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpMiddleware();
            app.UseMvc();



            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            var logger = loggerFactory.CreateLogger<ConsoleLogger>();
            logger.LogInformation("Logger configured successfully");
        }
    }
}
