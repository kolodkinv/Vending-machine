using Microsoft.EntityFrameworkCore;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
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
        
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Product>()
                .HasOne(a => a.Image)
                .WithOne(b => b.Product)
                .HasForeignKey<Image>(b => b.ProductId);

            base.OnModelCreating(modelBuilder);
        }
        */
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Money> Monies { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}