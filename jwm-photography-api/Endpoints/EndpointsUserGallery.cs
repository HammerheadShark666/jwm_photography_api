﻿using Asp.Versioning;
using FluentValidation;
using jwm_photography_api.Extensions;
using jwm_photography_api.Helper;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.UserGallery.AddUserGallery;
using jwm_photography_api.MediatR.UserGallery.AddUserGalleryPhoto;
using jwm_photography_api.MediatR.UserGallery.DeleteUserGallery;
using jwm_photography_api.MediatR.UserGallery.DeleteUserGalleryPhoto;
using jwm_photography_api.MediatR.UserGallery.GetUserGalleries;
using jwm_photography_api.MediatR.UserGallery.GetUserGallery;
using jwm_photography_api.MediatR.UserGallery.UpdateUserGallery;
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


        userGalleryGroup.MapPut("/update", async ([FromBody] UpdateUserGalleryRequest updateUserGalleryRequest, IMediator mediator, HttpContext httpContext) =>
        {
            var addUserGalleryResponse = await mediator.Send(new UpdateUserGalleryRequest(AuthenticationHelper.GetAccountId(httpContext), updateUserGalleryRequest.GalleryId, updateUserGalleryRequest.Name, updateUserGalleryRequest.Description));
            return Results.Ok(addUserGalleryResponse);
        })
        .Accepts<UpdateUserGalleryRequest>("application/json")
        .Produces<UpdateUserGalleryResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        .Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        .WithName("Update User Gallery")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Update User Gallery.",
            Description = "Update User Gallery.",
            Tags = [new() { Name = "JWM Photography - Update User Gallery" }]
        });


        userGalleryGroup.MapPost("/add/photo", async ([FromBody] AddUserGalleryPhotoRequest addUserGalleryPhotoRequest, IMediator mediator, HttpContext httpContext) =>
        {
            var addUserGalleryPhotoRequestWithAccountId = addUserGalleryPhotoRequest with { AccountId = AuthenticationHelper.GetAccountId(httpContext) };
            var addUserGalleryPhotoResponse = await mediator.Send(addUserGalleryPhotoRequestWithAccountId);
            return Results.Ok(addUserGalleryPhotoResponse);
        })
        .Accepts<AddUserGalleryPhotoRequest>("application/json")
        .Produces<AddUserGalleryPhotoResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        .Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        .WithName("Add User Gallery Photo")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Add User Gallery Photo.",
            Description = "Add User Gallery Photo.",
            Tags = [new() { Name = "JWM Photography - Add User Gallery Photo" }]
        });

        userGalleryGroup.MapDelete("/photo/{userGalleryId}/{PhotoId}", async ([FromRoute] int userGalleryId, [FromRoute] int photoId, IMediator mediator, HttpContext httpContext) =>
        {
            var deleteUserGalleryPhotoResponse = await mediator.Send(new DeleteUserGalleryPhotoRequest(AuthenticationHelper.GetAccountId(httpContext), userGalleryId, photoId));
            return Results.Ok(deleteUserGalleryPhotoResponse);
        })
        .Accepts<DeleteUserGalleryPhotoRequest>("application/json")
        .Produces<DeleteUserGalleryPhotoResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        .Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        .WithName("Delete User Gallery Photo")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Delete User Gallery Photo.",
            Description = "Delete User Gallery Photo.",
            Tags = [new() { Name = "JWM Photography - Delete User Gallery Photo" }]
        });
    }
}
