using Asp.Versioning;
using jwm_photography_api.Extensions;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Country.GetCountries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;

namespace jwm_photography_api.Endpoints;

public static class EndpointsCountry
{
    public static void ConfigureRoutes(this WebApplication webApplication)
    {
        var countryGroup = webApplication.MapGroup("v{version:apiVersion}/countries").WithTags("countries");

        countryGroup.MapGet("", async ([FromServices] IMediator mediator) =>
        {
            var countries = await mediator.Send(new GetCountriesRequest());
            return Results.Ok(countries.Countries);
        })
        .Produces<GetCountriesResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .WithName("GetCountries")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Get countries.",
            Description = "Get countries.",
            Tags = [new() { Name = "JWM Photography - Countries" }]
        });
    }
}