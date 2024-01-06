using Sellify.Domain.Common;

namespace Sellify.Domain;

public class Country : BaseDomainModel{

    public string? Iso2 {get; set;}

    public string? Iso3 {get; set;}
}