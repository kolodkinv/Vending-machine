using Microsoft.Extensions.DependencyInjection;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Repositories;
using Vending_Machine.Repositories.EF;

namespace Vending_Machine.Seller
{
    public static class VendingMachineExtensions
    {    
        /// <summary>
        /// Метод внедряющий зависимости продуктового автомата.
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TProduct">Тип продукта, продаваемого автоматом</typeparam>
        /// <typeparam name="TMoney">Тип денег с которыми работает автомат</typeparam>
        public static void AddVendingMachineEF<TProduct, TMoney>(this IServiceCollection services) 
            where TProduct : Product
            where TMoney: Money
        {
            services.AddScoped<IRepository<TProduct>, EFRepository<TProduct>>();
            services.AddScoped<IRepository<TMoney>, EFRepository<TMoney>>();
            services.AddScoped<IRepository<Basket>, EFRepository<Basket>>();
            services.AddScoped<VendingMachine<TProduct, TMoney>>();
        }
        
    }
}