namespace Sellify.Application.Features.ShoppingCars.Vms;

public class ShoppingCarVm
{
    public string? ShoppingCarId { get; set; }

    public List<ShoppingCarItemVm>? ShoppingCarItems { get; set;}

    public decimal Total
    {
        get
        {
            return Math.Round
            (
                ShoppingCarItems!.Sum(x => x.Precio * x.Cantidad) +
                (ShoppingCarItems!.Sum(x => x.Precio * x.Cantidad)*Convert.ToDecimal(0.18)) +
                (ShoppingCarItems!.Sum(x => x.Precio * x.Cantidad) < 100 ? 10 : 25), 2
            );
        }

        set
        {

        }
    }

    public int Cantidad
    {
        get { return ShoppingCarItems!.Sum( x => x.Cantidad);}
        set {}
    }

    public decimal SubTotal
    {
        get { return Math.Round(ShoppingCarItems!.Sum(x => x.Precio * x.Cantidad), 2); }
        set {}
    }

    public decimal Impuesto
    {
        get
        {
            return Math.Round(((ShoppingCarItems!.Sum(x => x.Precio * x.Cantidad)) * Convert.ToDecimal(0.18)), 2);
        }
        set {}
    }

    public decimal PrecioEnvio
    {
        get
        {
            return (ShoppingCarItems!.Sum(x => x.Precio * x.Cantidad)) < 100 ? 10 : 25;
        }

        set
        {
        }
    }
}