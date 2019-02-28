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
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Money> Monies { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Image> Images { get; set; }
        //public DbSet<MoneyBasket> MoneyBaskets { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoneyBasket>()
                .HasKey(mb => new { mb.MoneyId, mb.BasketId });
            modelBuilder.Entity<MoneyBasket>()
                .HasOne(bc => bc.Money)
                .WithMany(b => b.MoneyBaskets)
                .HasForeignKey(bc => bc.MoneyId);
            modelBuilder.Entity<MoneyBasket>()
                .HasOne(bc => bc.Basket)
                .WithMany(c => c.MoneyBaskets)
                .HasForeignKey(bc => bc.BasketId);
            
            modelBuilder.Entity<ProductBasket>()
                .HasKey(mb => new { mb.ProductId, mb.BasketId });
            modelBuilder.Entity<ProductBasket>()
                .HasOne(bc => bc.Product)
                .WithMany(b => b.ProductBaskets)
                .HasForeignKey(bc => bc.ProductId);
            modelBuilder.Entity<ProductBasket>()
                .HasOne(bc => bc.Basket)
                .WithMany(c => c.ProductBaskets)
                .HasForeignKey(bc => bc.BasketId);
        }
    }
}