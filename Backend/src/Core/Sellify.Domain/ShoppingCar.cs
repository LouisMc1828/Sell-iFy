using Sellify.Domain.Common;

namespace Sellify.Domain;

public class ShoppingCar : BaseDomainModel{

    public Guid? ShoppingCarMasterId { get; set; }

    public virtual ICollection<ShoppingCarItem>? ShoppingCarItems { get; set; }
    
}