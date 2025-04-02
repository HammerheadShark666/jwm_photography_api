using Asp.Versioning;
using FluentValidation;
using jwm_photography_api.Extensions;
using jwm_photography_api.Helper;
using jwm_photography_api.Helpers.Exceptions;
using jwm_photography_api.MediatR.Authentication.Login;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Net;

namespace jwm_photography_api.Endpoints;

public static class EndpointsAuthenticate
{
    public static void ConfigureRoutes(this WebApplication webApplication)
    {
        var loginGroup = webApplication.MapGroup("v{version:apiVersion}/").WithTags("authenticate");

        loginGroup.MapPost("/login", async (LoginRequest loginRequest, IMediator mediator, HttpContext httpContext, HttpRequest httpRequest) =>
        {
            var loginRequestWithIpAddress = loginRequest with { IpAddress = AuthenticationHelper.IpAddress(httpRequest, httpContext) };
            var loginResponse = await mediator.Send(loginRequestWithIpAddress);
            return Results.Ok(loginResponse);
        })
        .Accepts<LoginRequest>("application/json")
        .Produces<LoginResponse>((int)HttpStatusCode.OK)
        .Produces<BadRequestException>((int)HttpStatusCode.BadRequest)
        .Produces<ValidationException>((int)HttpStatusCode.BadRequest)
        .Produces<ArgumentException>((int)HttpStatusCode.BadRequest)
        .WithName("Login")
        .WithApiVersionSet(webApplication.GetApiVersionSet())
        .MapToApiVersion(new ApiVersion(1, 0))
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Login to site.",
            Description = "Login to site.",
            Tags = [new() { Name = "JWM Photography - Login" }]
        });
    }
}
