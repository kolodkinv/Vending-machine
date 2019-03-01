using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vending_Machine.Middlewares;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Repositories.EF;
using Vending_Machine.Seller;

namespace Vending_Machine
{
    public class Startup
    {
        private string _contentRootPath;
        
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _contentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            
            services.AddVendingMachineEF<Drink, Money, Image, DrinksMachine>();

//            string connection =
//                "Server=localhost,1433;AttachDBFilename=%CONTENTROOTPATH%/App_Data/Machine4.mdf;User Id=SA;Password=ZxcVda!@#123";
//            if(connection.Contains("%CONTENTROOTPATH%"))
//            {
//                connection = connection.Replace("%CONTENTROOTPATH%", _contentRootPath);
//            }   
            //services.AddDbContext<MachineContext>(opt => opt.UseSqlServer(connection));
            
            services.AddDbContext<MachineContext>(
                opt => opt.UseSqlServer("Server=localhost,1433;Database=Machine4;User Id=SA;Password=ZxcVda!@#123"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}