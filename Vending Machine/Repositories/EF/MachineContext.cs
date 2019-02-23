using Microsoft.EntityFrameworkCore;
using Vending_Machine.Models;
using Vending_Machine.Models.Product;
using Vending_Machine.Seller;

namespace Vending_Machine.Repositories.EF
{
    public class MachineContext : DbContext
    {
        public MachineContext(DbContextOptions<MachineContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Money> Monies { get; set; }
        public DbSet<Basket> Baskets { get; set; }
    }
}