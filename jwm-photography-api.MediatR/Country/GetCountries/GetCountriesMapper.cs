using AutoMapper;

namespace jwm_photography_api.MediatR.Country.GetCountries;

public class GetCountriesMapper : Profile
{
    public GetCountriesMapper()
    {
        base.CreateMap<List<Domain.Country>, GetCountriesResponse>()
            .ForCtorParam(nameof(GetCountriesResponse.Countries),
                    opt => opt.MapFrom(src => src));

        base.CreateMap<Domain.Country, CountryResponse>();
    }
}