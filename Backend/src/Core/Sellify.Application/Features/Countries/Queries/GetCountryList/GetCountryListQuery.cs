using MediatR;
using Sellify.Application.Features.Countries.Vms;

namespace Sellify.Application.Features.Countries.Queries.GetCountryList;

public class GetCountryListQuery : IRequest<IReadOnlyList<CountryVm>>
{
    
}