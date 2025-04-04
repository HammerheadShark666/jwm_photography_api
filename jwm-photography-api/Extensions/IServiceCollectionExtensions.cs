using Asp.Versioning;
using FluentValidation;
using jwm_photography_api.Data.Contexts;
using jwm_photography_api.Data.Repository;
using jwm_photography_api.Data.Repository.Interfaces;
using jwm_photography_api.Data.UnitOfWork;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helper;
using jwm_photography_api.Helpers.Interfaces;
using jwm_photography_api.Helpers.Swagger;
using jwm_photography_api.MediatR.Authentication.Login;
using jwm_photography_api.MediatR.Palette.GetPalettes;
using jwm_photography_api.MediatR.Service;
using jwm_photography_api.MediatR.Service.Interfaces;
using jwm_photography_api.Middleware;
using MediatR;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace jwm_photography_api.Extensions;

public static class IServiceCollectionExtensions
{
    public static void ConfigureJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = EnvironmentVariables.JwtIssuer,
                    ValidAudience = EnvironmentVariables.JwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.JwtSymmetricSecurityKey))
                };
            });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();
            options.SupportNonNullableReferenceTypes();
        });
    }
    public static void ConfigureDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<JwmPhotographyApiDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(Constants.DatabaseConnectionString),
            options => options.EnableRetryOnFailure()
            .MigrationsAssembly(typeof(JwmPhotographyApiDbContext).Assembly.FullName)));
    }

    public static void ConfigureDI(this IServiceCollection services)
    {
        services.AddScoped<IUserGalleryRepository, UserGalleryRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IFavouriteRepository, FavouriteRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAzureStorageBlobHelper, AzureStorageBlobHelper>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddMemoryCache();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddMemoryCache();
    }

    public static void ConfigureApplicationInsights(this IServiceCollection services)
    {
        var options = new ApplicationInsightsServiceOptions { ConnectionString = EnvironmentVariables.ApplicationInsightsConnectionString };
        services.AddApplicationInsightsTelemetry(options: options);
    }

    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers()
           .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
           .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
    }

    public static void ConfigureMVC(this IServiceCollection services)
    {
        services.AddMvc(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        })
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(GetPalettesMapper)));
    }

    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });
    }

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LoginValidator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPalettesRequest).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehaviour<,>));
    }

    public static void ConfigureExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
    }
}