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
        /// <typeparam name="TImage">Тип картинок-этикетов, которые показывает автомат на витрине</typeparam>
        /// <typeparam name="TMachine">Тип торгового автомата</typeparam>
        public static void AddVendingMachineEF<TProduct, TMoney, TImage, TMachine>(this IServiceCollection services) 
            where TProduct : Product
            where TMoney: Money
            where TImage: Image
            where TMachine : VendingMachine<TProduct, TMoney, TImage>
        {
            services.AddScoped<IRepository<TProduct>, EFRepository<TProduct>>();
            services.AddScoped<IRepository<TMoney>, EFRepository<TMoney>>();
            services.AddScoped<IRepository<TImage>, EFRepository<TImage>>();
            services.AddScoped<VendingMachine<TProduct, TMoney, TImage>, TMachine>();         
            services.AddScoped<UnitOfWorkEF>();
        }
        
    }
}