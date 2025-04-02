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



        //bookGroup.MapGet("/{id}", async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
        //{
        //    var getBookResponse = await mediator.Send(new GetBookRequest(id));
        //    return Results.Ok(getBookResponse);
        //})
        //.Produces<GetBookResponse>((int)HttpStatusCode.OK)
        //.Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        //.Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        //.WithName("GetBook")
        //.WithApiVersionSet(webApplication.GetApiVersionSet())
        //.MapToApiVersion(new ApiVersion(1, 0))
        //.RequireAuthorization()
        //.WithOpenApi(x => new OpenApiOperation(x)
        //{
        //    Summary = "Get a book based on id.",
        //    Description = "Gets a book based on its id.",
        //    Tags = [new() { Name = "Microservice Order System - Books" }]
        //});

        //bookGroup.MapGet("/title/{criteria}", async ([FromRoute] string criteria, [FromServices] IMediator mediator) =>
        //{
        //    var searchBookTitleResponse = await mediator.Send(new SearchBookTitleRequest(criteria));
        //    return Results.Ok(searchBookTitleResponse.Books);
        //})
        //.Accepts<SearchBookTitleRequest>("application/json")
        //.Produces<SearchBookTitleResponse>((int)HttpStatusCode.OK)
        //.Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        //.Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        //.WithName("SearchBookTitle")
        //.WithApiVersionSet(webApplication.GetApiVersionSet())
        //.MapToApiVersion(new ApiVersion(1, 0))
        //.RequireAuthorization()
        //.WithOpenApi(x => new OpenApiOperation(x)
        //{
        //    Summary = "Search for books based on title.",
        //    Description = "Gets books based on title.",
        //    Tags = [new() { Name = "Microservice Order System - Books" }]
        //});

        //bookGroup.MapPost("/add", async (AddBookRequest addBookRequest, IMediator mediator) =>
        //{
        //    var addBookResponse = await mediator.Send(addBookRequest);
        //    return Results.Ok(addBookResponse);
        //})
        //.Accepts<AddBookRequest>("application/json")
        //.Produces<AddBookResponse>((int)HttpStatusCode.OK)
        //.Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        //.Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        //.Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        //.WithName("AddBook")
        //.WithApiVersionSet(webApplication.GetApiVersionSet())
        //.MapToApiVersion(new ApiVersion(1, 0))
        //.RequireAuthorization()
        //.WithOpenApi(x => new OpenApiOperation(x)
        //{
        //    Summary = "Add a book.",
        //    Description = "Adds a book.",
        //    Tags = [new() { Name = "Microservice Order System - Books" }]
        //});

        //bookGroup.MapPut("/update", async (UpdateBookRequest updateBookRequest, IMediator mediator) =>
        //{
        //    var updateBookResponse = await mediator.Send(updateBookRequest);
        //    return Results.Ok(updateBookResponse);
        //})
        //.Accepts<UpdateBookRequest>("application/json")
        //.Produces<UpdateBookResponse>((int)HttpStatusCode.OK)
        //.Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        //.Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        //.Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        //.Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        //.WithName("UpdateBook")
        //.WithApiVersionSet(webApplication.GetApiVersionSet())
        //.MapToApiVersion(new ApiVersion(1, 0))
        //.RequireAuthorization()
        //.WithOpenApi(x => new OpenApiOperation(x)
        //{
        //    Summary = "Update a book.",
        //    Description = "Updates a book.",
        //    Tags = [new() { Name = "Microservice Order System - Books" }]
        //});

        //bookGroup.MapDelete("/{id}", async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
        //{
        //    var deleteBookResponse = await mediator.Send(new DeleteBookRequest(id));
        //    return Results.Ok(deleteBookResponse);
        //})
        //.Accepts<DeleteBookRequest>("application/json")
        //.Produces((int)HttpStatusCode.OK)
        //.Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        //.Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        //.Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        //.WithName("DeleteBook")
        //.WithApiVersionSet(webApplication.GetApiVersionSet())
        //.MapToApiVersion(new ApiVersion(1, 0))
        //.RequireAuthorization()
        //.WithOpenApi(x => new OpenApiOperation(x)
        //{
        //    Summary = "Delete a book.",
        //    Description = "Deletes a book.",
        //    Tags = [new() { Name = "Microservice Order System - Books" }]
        //});
    }
}