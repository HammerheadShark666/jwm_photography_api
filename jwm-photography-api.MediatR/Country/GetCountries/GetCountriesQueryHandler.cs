using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace jwm_photography_api.MediatR.Country.GetCountries;

public class GetCountriesQueryHandler(IUnitOfWork unitOfWork,
                                      ILogger<GetCountriesQueryHandler> logger,
                                      IMapper mapper) : IRequestHandler<GetCountriesRequest, GetCountriesResponse>
{
    public async Task<GetCountriesResponse> Handle(GetCountriesRequest getCountryRequest, CancellationToken cancellationToken)
    {
        var countries = await unitOfWork.Countries.AllSortedAsync();
        if (countries.Count == 0)
        {
            logger.LogError("Countries not found.");
            throw new NotFoundException("Countries not found.");
        }

        return mapper.Map<GetCountriesResponse>(countries);
    }
}