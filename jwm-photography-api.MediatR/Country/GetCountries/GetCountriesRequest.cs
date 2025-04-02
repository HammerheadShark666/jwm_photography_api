using MediatR;

namespace jwm_photography_api.MediatR.Country.GetCountries;

public record GetCountriesRequest() : IRequest<GetCountriesResponse>;