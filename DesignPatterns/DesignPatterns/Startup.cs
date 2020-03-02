using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DesignPatterns.Factory;
using DesignPatterns.Factory.Factory;
using DesignPatterns.Factory.Instrument;
using DesignPatterns.Factory.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DesignPatterns
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMvc();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"}); });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            //DirectRegistration(builder);
            ScanningAssemblyRegistration(builder);

            AutofacContainer = builder.Build();
            return new AutofacServiceProvider(AutofacContainer);
        }

        private void DirectRegistration(ContainerBuilder builder)
        {
            builder.RegisterType<GuitarFactory>().As<IInstrumentFactory>();
            builder.RegisterType<TrumpetFactory>().As<IInstrumentFactory>();
            builder.RegisterType<InstrumentService>().As<IInstrumentService>();
        }

        private void ScanningAssemblyRegistration(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Factory"))
                .Except<FluteFactory>()
                .As<IInstrumentFactory>();

            builder.RegisterType<InstrumentService>().As<IInstrumentService>();
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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }
    }
}
