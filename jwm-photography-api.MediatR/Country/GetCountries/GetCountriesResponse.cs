namespace jwm_photography_api.MediatR.Country.GetCountries;

public record GetCountriesResponse(List<CountryResponse> Countries);

public record CountryResponse(int Id, string Name);