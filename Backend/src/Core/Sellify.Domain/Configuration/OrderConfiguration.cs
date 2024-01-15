using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sellify.Domain.Configuration;


public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.OrderAddress, x => {
            x.WithOwner();
        });

        builder.HasMany(o => o.OrderItems).WithOne()
        .OnDelete(DeleteBehavior.Cascade);

        builder.Property(s => s.Status).HasConversion(
            o => o.ToString(),
            o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
        );
    }
}

/*public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.OrderAddress, x =>
        {
            x.WithOwner();
        });

        builder.HasMany(o => o.OrderItems)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);

        builder.Property(s => s.Status).HasConversion(
            o => o.ToString(),
            o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
        );
    }
}

Cap 37/Seccion 4(Fin) - Services API

C:\Users\USER\Desktop\Sell-iFy\Backend\src\Core\Sellify.Domain\Configuration\OrderConfiguration.cs(20,63): warning CS8604:  
Posible argumento de referencia nulo para el parámetro "value" en "object Enum.Parse(Type enumType, string value)". [C:\Use 
rs\USER\Desktop\Sell-iFy\Backend\src\Core\Sellify.Domain\Sellify.Domain.csproj]
*/