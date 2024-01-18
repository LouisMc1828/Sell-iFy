using AutoMapper;
using MediatR;
using Sellify.Application.Features.Countries.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Countries.Queries.GetCountryList;

public class GetCountryListQueryHandler : IRequestHandler<GetCountryListQuery, IReadOnlyList<CountryVm>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCountryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CountryVm>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
    {
        var countries = await _unitOfWork.Repository<Country>().GetAsync(
            null,
            x => x.OrderBy(y => y.Name),
            string.Empty,
            false
        );

        return _mapper.Map<IReadOnlyList<CountryVm>>(countries);
    }
}