using Asp.Versioning;
using jwm_photography_api.Extensions;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Category.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;

namespace jwm_photography_api.Endpoints;

public static class EndpointsCategory
{
    public static void ConfigureRoutes(this WebApplication webApplication)
    {
        var categoryGroup = webApplication.MapGroup("v{version:apiVersion}/categories").WithTags("categories");

        categoryGroup.MapGet("", async ([FromServices] IMediator mediator) =>
        {
            var categories = await mediator.Send(new GetCategoriesRequest());
            return Results.Ok(categories.Categories);
        })
        .Produces<GetCategoriesResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<NotFoundException>((int)HttpStatusCode.NotFound)
        .WithName("GetCategories")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Get categories.",
            Description = "Get categories.",
            Tags = [new() { Name = "JWM Photography - Categories" }]
        });
    }
}