using Asp.Versioning;
using jwm_photography_api.Extensions;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Photo.GetRandomPhotos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;

namespace jwm_photography_api.Endpoints;

public static class EndpointsPhoto
{
    public static void ConfigureRoutes(this WebApplication webApplication)
    {
        var galleryGroup = webApplication.MapGroup("v{version:apiVersion}/photos").WithTags("photos");

        galleryGroup.MapGet("/random/{numberOfLandscape}/{numberOfPortrait}/{numberOfSquare}",
        async ([FromRoute] int numberOfLandscape, int numberOfPortrait, int numberOfSquare, [FromServices] IMediator mediator) =>
        {
            var photos = await mediator.Send(new GetRandomPhotosRequest(numberOfLandscape, numberOfPortrait, numberOfSquare));
            return Results.Ok(photos);
        })
       .Produces<GetRandomPhotosResponse>((int)HttpStatusCode.OK)
       .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
       .Produces<NotFoundException>((int)HttpStatusCode.NotFound)
       .WithName("GetRandomPhotos")
       .WithApiVersionSet(webApplication.GetApiVersionSet())
       .MapToApiVersion(new ApiVersion(1, 0))
       .RequireAuthorization()
       .WithOpenApi(x => new OpenApiOperation(x)
       {
           Summary = "Get random photos.",
           Description = "Get random photos.",
           Tags = [new() { Name = "JWM Photography - Random Photos" }]
       });

    }
}


