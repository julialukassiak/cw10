using Microsoft.EntityFrameworkCore;
using cw10.Models;

namespace cw10.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Role> Roles { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>().HasKey(sc => new { sc.AccountId, sc.ProductId });
        modelBuilder.Entity<ProductCategory>().HasKey(pc => new { pc.ProductId, pc.CategoryId });

        modelBuilder.Entity<Role>().HasMany(r => r.Accounts).WithOne(a => a.Role).HasForeignKey(a => a.RoleId);
        modelBuilder.Entity<Account>().HasMany(a => a.ShoppingCarts).WithOne(sc => sc.Account).HasForeignKey(sc => sc.AccountId);
        modelBuilder.Entity<Product>().HasMany(p => p.ShoppingCarts).WithOne(sc => sc.Product).HasForeignKey(sc => sc.ProductId);
        modelBuilder.Entity<Category>().HasMany(c => c.ProductCategories).WithOne(pc => pc.Category).HasForeignKey(pc => pc.CategoryId);
        modelBuilder.Entity<Product>().HasMany(p => p.ProductCategories).WithOne(pc => pc.Product).HasForeignKey(pc => pc.ProductId);
    }
}