using Asp.Versioning;
using FluentValidation;
using jwm_photography_api.Extensions;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Gallery.GetGalleries;
using jwm_photography_api.MediatR.Gallery.GetGallery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;

namespace jwm_photography_api.Endpoints;

public static class EndpointsGallery
{
    public static void ConfigureRoutes(this WebApplication webApplication)
    {
        var galleryGroup = webApplication.MapGroup("v{version:apiVersion}/galleries").WithTags("galleries");

        galleryGroup.MapGet("", async ([FromServices] IMediator mediator) =>
        {
            var galleries = await mediator.Send(new GetGalleriesRequest());
            return Results.Ok(galleries.Galleries);
        })
        .Produces<GetGalleriesResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        .WithName("GetGalleries")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Get galleries.",
            Description = "Get galleries.",
            Tags = [new() { Name = "JWM Photography - Galleries" }]
        });

        galleryGroup.MapGet("/gallery/{id}", async ([FromRoute] int id, [FromServices] IMediator mediator) =>
        {
            var gallery = await mediator.Send(new GetGalleryRequest(id));
            return Results.Ok(gallery);
        })
        .Produces<GetGalleriesResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        .Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        .WithName("GetGallery")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Get gallery.",
            Description = "Get gallery.",
            Tags = [new() { Name = "JWM Photography - Gallery" }]
        });
    }
}