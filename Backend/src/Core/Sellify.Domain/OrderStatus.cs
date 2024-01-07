using System.Runtime.Serialization;
using Sellify.Domain.Common;

namespace Sellify.Domain;

public enum OrderStatus {
    [EnumMember(Value ="Pendiente")]
    Pending,
    [EnumMember(Value ="Pago efectuado exitosamente")]
    Completed,
    [EnumMember(Value ="El producto fue enviado")]
    Sent,
    [EnumMember(Value ="Sucedió un error durante el pago")]
    Error

}