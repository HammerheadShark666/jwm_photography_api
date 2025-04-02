using Asp.Versioning;
using FluentValidation;
using jwm_photography_api.Extensions;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Favourite.AddFavouritePhoto;
using jwm_photography_api.MediatR.Favourite.DeleteFavouritePhoto;
using jwm_photography_api.MediatR.Favourite.GetFavouritePhotos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;

namespace jwm_photography_api.Endpoints;

public static class EndpointsFavourite
{
    public static void ConfigureRoutes(this WebApplication webApplication)
    {
        var favouriteGroup = webApplication.MapGroup("v{version:apiVersion}/favourites").WithTags("favourite");

        favouriteGroup.MapGet("", async ([FromServices] IMediator mediator, HttpContext httpContext) =>
        {
            var accountId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (accountId == null)
            {
                throw new BadRequestException("Account Id not found.");
            }

            var favouritesPhotos = await mediator.Send(new GetFavouritePhotosRequest(new Guid(accountId)));
            return Results.Ok(favouritesPhotos.FavouritePhotos);
        })
        .Produces<GetFavouritePhotosResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        .WithName("GetFavouritePhotos")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Get favourite photos for the user.",
            Description = "Get favourite photos for the user.",
            Tags = [new() { Name = "JWM Photography - Get favourite photos for the user." }]
        });

        favouriteGroup.MapPost("/add", async ([FromBody] AddFavouritePhotoRequest addFavouritePhotoRequest, IMediator mediator) =>
        {
            var addFavouritePhotoResponse = await mediator.Send(addFavouritePhotoRequest);
            return Results.Ok(addFavouritePhotoResponse);
        })
        .Accepts<AddFavouritePhotoRequest>("application/json")
        .Produces<AddFavouritePhotoResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        .Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        .WithName("Add Favourite Photo")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Add Favourite Photo.",
            Description = "Add Favourite Photo.",
            Tags = [new() { Name = "JWM Photography - Add Favourite Photo" }]
        });

        favouriteGroup.MapDelete("", async ([FromBody] DeleteFavouritePhotoRequest deleteFavouritePhotoRequest, IMediator mediator) =>
        {
            var deleteFavouritePhotoResponse = await mediator.Send(deleteFavouritePhotoRequest);
            return Results.Ok(deleteFavouritePhotoResponse);
        })
        .Accepts<DeleteFavouritePhotoRequest>("application/json")
        .Produces<DeleteFavouritePhotoResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        .Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        .WithName("Delete Favourite Photo")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Delete Favourite Photo.",
            Description = "Delete Favourite Photo.",
            Tags = [new() { Name = "JWM Photography - Delete Favourite Photo" }]
        });
    }
}
