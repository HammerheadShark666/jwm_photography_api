using Asp.Versioning;
using jwm_photography_api.Extensions;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Palette.GetPalettes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;

namespace jwm_photography_api.Endpoints;

public static class EndpointsPalette
{
    public static void ConfigureRoutes(this WebApplication webApplication)
    {
        var PaletteGroup = webApplication.MapGroup("v{version:apiVersion}/palettes").WithTags("palettes");

        PaletteGroup.MapGet("", async ([FromServices] IMediator mediator) =>
        {
            var palettes = await mediator.Send(new GetPalettesRequest());
            return Results.Ok(palettes.Palettes);
        })
        .Produces<GetPalettesResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        .WithName("GetPalettes")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Get palettes.",
            Description = "Get palettes.",
            Tags = [new() { Name = "JWM Photography - Palettes" }]
        });
    }
}