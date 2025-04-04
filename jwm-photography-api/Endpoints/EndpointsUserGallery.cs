using Asp.Versioning;
using FluentValidation;
using jwm_photography_api.Extensions;
using jwm_photography_api.Helper;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.UserGallery.AddUserGallery;
using jwm_photography_api.MediatR.UserGallery.DeleteUserGallery;
using jwm_photography_api.MediatR.UserGallery.GetUserGalleries;
using jwm_photography_api.MediatR.UserGallery.GetUserGallery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;

namespace jwm_photography_api.Endpoints;

public static class EndpointsUserGallery
{
    public static void ConfigureRoutes(this WebApplication webApplication)
    {
        var userGalleryGroup = webApplication.MapGroup("v{version:apiVersion}/user-galleries").WithTags("user-gallery");

        userGalleryGroup.MapGet("", async ([FromServices] IMediator mediator, HttpContext httpContext) =>
        {
            var userGalleries = await mediator.Send(new GetUserGalleriesRequest(AuthenticationHelper.GetAccountId(httpContext)));
            return Results.Ok(userGalleries.UserGalleries);
        })
        .Produces<GetUserGalleriesResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        .RequireAuthorization()
        .WithName("GetUserGalleries")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Get user galleries.",
            Description = "Get user galleries.",
            Tags = [new() { Name = "JWM Photography - Get User Galleries" }]
        });

        userGalleryGroup.MapGet("/Gallery/{galleryId}", async (long galleryId, [FromServices] IMediator mediator, HttpContext httpContext) =>
        {
            var userGallery = await mediator.Send(new GetUserGalleryRequest(AuthenticationHelper.GetAccountId(httpContext), galleryId));
            return Results.Ok(userGallery);
        })
        .Produces<GetUserGalleryResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        .RequireAuthorization()
        .WithName("GetUserGallery")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Get user gallery.",
            Description = "Get user gallery.",
            Tags = [new() { Name = "JWM Photography - Get User Gallery" }]
        });


        userGalleryGroup.MapPost("/add", async ([FromBody] AddUserGalleryRequest addUserGalleryRequest, IMediator mediator, HttpContext httpContext) =>
        {
            var addUserGalleryResponse = await mediator.Send(new AddUserGalleryRequest(AuthenticationHelper.GetAccountId(httpContext), addUserGalleryRequest.Name, addUserGalleryRequest.Description));
            return Results.Ok(addUserGalleryResponse);
        })
        .Accepts<AddUserGalleryRequest>("application/json")
        .Produces<AddUserGalleryResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        .Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        .WithName("Add User Gallery")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Add User Gallery.",
            Description = "Add User Gallery.",
            Tags = [new() { Name = "JWM Photography - Add User Gallery" }]
        });

        userGalleryGroup.MapDelete("/{userGalleryId}", async ([FromRoute] long userGalleryId, IMediator mediator, HttpContext httpContext) =>
        {
            var deleteUserGalleryResponse = await mediator.Send(new DeleteUserGalleryRequest(AuthenticationHelper.GetAccountId(httpContext), userGalleryId));
            return Results.Ok(deleteUserGalleryResponse);
        })
        .Accepts<DeleteUserGalleryRequest>("application/json")
        .Produces<DeleteUserGalleryResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        .Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        .WithName("Delete User Gallery")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Delete User Gallery.",
            Description = "Delete User Gallery.",
            Tags = [new() { Name = "JWM Photography - Delete User Gallery" }]
        });


        //Update User Gallery


        //Add Photo to User Gallery

        //Remove Photo from User Gallery
    }
}
