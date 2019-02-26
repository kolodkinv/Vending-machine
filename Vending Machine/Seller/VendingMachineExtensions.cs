using Microsoft.Extensions.DependencyInjection;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Repositories;
using Vending_Machine.Repositories.EF;
using Vending_Machine.Storage;

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
        /// <typeparam name="TProductRepository">Хранилище продуктов</typeparam>
        /// <typeparam name="TMoneyRepository">Хранилище денег</typeparam>
        public static void AddVendingMachine<TProduct, TMoney, TProductRepository, TMoneyRepository>(this IServiceCollection services) 
            where TProduct : Product
            where TMoney: Money
            where TProductRepository : class, IRepository<TProduct>
            where TMoneyRepository : class, IRepository<TMoney>
        {
            services.AddScoped<Storage<TProduct>>();
            services.AddScoped<Storage<TMoney>>();
            services.AddScoped<IRepository<TProduct>, EFRepository<TProduct>>();
            services.AddScoped<IRepository<TMoney>, EFRepository<TMoney>>();
            services.AddScoped<VendingMachine<TProduct, TMoney>>();
            services.AddScoped<IRepository<Image>, ImageRepository>();
        }
        
    }
}