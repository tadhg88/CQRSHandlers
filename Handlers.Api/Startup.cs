using Autofac;
using Autofac.Extensions.DependencyInjection;
using CommandHandlersMapping.DependencyModules;
using CommandHandlersMapping.Tests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Handlers.Api
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
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // RegisterAndResolveLocally();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterAndResolveLocally()
        {
            var containerBuilder = new ContainerBuilder();

            //containerBuilder.RegisterType<MessageHandler>().As<IHandler>();

            // Creating a new AutofacServiceProvider makes the container
            // available to your app using the Microsoft IServiceProvider
            // interface so you can use those abstractions rather than
            // binding directly to Autofac.
            containerBuilder.RegisterType<MyTest>().As<ITest>();

            var container1 = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container1);

            //var ic = serviceProvider.GetService<ITest>();
            //ic.DoStuff("do iiiit");

            using (var scope = container1.BeginLifetimeScope())
            {
                var writer = scope.Resolve<ITest>();
                writer.WriteToOutput("do iiiit");
            }
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ModuleBase>();
        }
    }
}
