using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sellify.Domain;

namespace Sellify.Infrastructure.Persistence;

public class SellifyDbContext : IdentityDbContext<Usuario>{


    public SellifyDbContext(DbContextOptions<SellifyDbContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>()
            .HasMany(p => p.Products)
            .WithOne(r => r.Category)
            .HasForeignKey(r => r.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Product>()
            .HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Product>()
            .HasMany(p => p.Images)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ShoppingCar>()
            .HasMany(p => p.ShoppingCarItems)
            .WithOne(r => r.ShoppingCar)
            .HasForeignKey(r => r.ShoppingCarId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);



        builder.Entity<Usuario>().Property(x => x.Id).HasMaxLength(36);
        builder.Entity<Usuario>().Property(x => x.NormalizedUserName).HasMaxLength(90);
        builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(36);
        builder.Entity<IdentityRole>().Property(x => x.NormalizedName).HasMaxLength(90);
    }


    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Image>? Images { get; set; }
    public DbSet<Address>? Addresses { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<OrderItem>? OrderItems { get; set; }
    public DbSet<Review>? Reviews { get; set; }
    public DbSet<ShoppingCar>? ShoppingCars { get; set; }
    public DbSet<ShoppingCarItem>? ShoppingCarItems { get; set; }
    public DbSet<Country>? Countries { get; set; }
    public DbSet<OrderAddress>? OrderAddress { get; set;}
}
