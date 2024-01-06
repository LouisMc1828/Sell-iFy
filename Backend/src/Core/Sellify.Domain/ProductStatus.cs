using System.Runtime.Serialization;

namespace Sellify.Domain;

public enum ProductStatus
{
    [EnumMember(Value = "Producto Inactivo")]
    Inactivo,

    [EnumMember(Value = "Producto Activo")]
    Activo
}