using Sellify.Application.Features.Images.Queries.Vms;
using Sellify.Application.Features.Reviews.Queries.Vms;
using Sellify.Application.Models.Product;
using Sellify.Domain;

namespace Sellify.Application.Features.Products.Queries.Vms;
public class ProductVm
{

    public int Id { get; set; }

    public string? Nombre { get; set; }

    public decimal Precio { get; set; }

    public int Rating { get; set; }

    public string? Vendedor { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<ReviewVm>? Reviews { get; set; }

    public virtual ICollection<ImageVm>? Images { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryNombre { get; set; }

    public int NumeroReviews { get; set; }

    public ProductStatus Status { get; set; }

    public string StatusLabel {
        get {
            switch(Status)
            {
                case ProductStatus.Activo: {
                    return ProductStatusLabel.ACTIVO;
                }
                case ProductStatus.Inactivo: {
                    return ProductStatusLabel.INACTIVO;
                }
                default: return ProductStatusLabel.INACTIVO;
            }
        }
        set {}
    }
}