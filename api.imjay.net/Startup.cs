using Api.Imjay.Net.Domain.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;

namespace Api.Imjay.Net
{

    public partial class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            ConfigureAuth(services);
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var container = new Container();
            container.Configure(_ =>
            {
                //config.Scan(_ =>
                //{
                //    _.AssemblyContainingType(typeof(Startup));
                //    _.WithDefaultConventions();
                //    _.AddAllTypesOf<IGamingService>();
                //    _.ConnectImplementationsToTypesClosing(typeof(IValidator<>)); 
                //});
                //_.ConnectImplementationsToTypesClosing(typeof(IValidator<>));

                _.AddRegistry(new DIServiceRegistry(Configuration));
                _.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseCors("*");
            //app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }


    }
}
