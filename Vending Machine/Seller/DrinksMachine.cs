using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Repositories.EF;

namespace Vending_Machine.Seller
{
    /// <summary>
    /// Торговый автомат для продажи напитков
    /// </summary>
    public class DrinksMachine : VendingMachine<Drink, Money, Image>
    {
        private readonly UnitOfWorkEF _db;
        
        public DrinksMachine(UnitOfWorkEF db) : base(db)
        {
            _db = db;
        }
    }
}