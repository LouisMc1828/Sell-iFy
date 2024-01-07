using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sellify.Domain.Common;

namespace Sellify.Domain;

public class Category : BaseDomainModel{

    [Column(TypeName ="NVARCHAR(255)")]
    public string? Nombre {get; set;}

    public virtual ICollection<Product>? Products {get; set;}
}