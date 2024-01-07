using System.ComponentModel.DataAnnotations.Schema;
using Sellify.Domain.Common;

namespace Sellify.Domain;

public class ShoppingCarItem : BaseDomainModel{
    public string? Producto { get; set; }
    
    [Column(TypeName ="DECIMAL(10,2)")]
    public decimal Precio { get; set; }

    public int Cantidad { get; set;}

    public string? Categoria { get; set; }

    public string? Imagen { get; set; }

    public Guid? ShoppingCarMasterId { get; set; }

    public int ShoppingCarId { get; set; }
    public virtual ShoppingCar? ShoppingCar { get; set; }

    public int ProductId { get; set; }

    public int Stock { get; set; }

}