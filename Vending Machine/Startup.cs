using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Storage;
using Vending_Machine.Repositories;
using Vending_Machine.Repositories.EF;
using Vending_Machine.Seller;

namespace Vending_Machine
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            
            services.AddVendingMachine<Drink, Money, DrinkRepository, MoneyRepository>();
            services.AddScoped<IRepository<Image>, ImageRepository>();
            /*
            services.AddScoped<VendingMachine<Drink, Money>>();
            services.AddScoped<Storage<Drink>>();
            services.AddScoped<Storage<Money>>();
            services.AddScoped<IRepository<Drink>, DrinkRepository>();
            services.AddScoped<IRepository<Money>, MoneyRepository>();
            */
            //services.AddDbContext<MachineContext>(
            //    opt => opt.UseSqlServer("Server=172.17.0.2,1433;Database=Machine4;User Id=SA;Password=ZxcVda!@#123"));
            services.AddDbContext<MachineContext>(options =>
                options.UseMySql(
                    "server=172.17.0.2;database=crypto;user=crypto;password=test;Charset=utf8;"
                )
            );
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