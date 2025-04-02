using Asp.Versioning;
using Asp.Versioning.Builder;
using jwm_photography_api.Middleware;

namespace jwm_photography_api.Extensions;

public static class AppExtensions
{
    public static void ConfigureSwagger(this WebApplication webApplication)
    {
        if (webApplication.Environment.IsDevelopment())
        {
            webApplication.UseSwagger(options => options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0)
            .UseSwaggerUI(options =>
            {
                options.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);

                var descriptions = webApplication.DescribeApiVersions();

                // Build a swagger endpoint for each discovered API version
                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
        }
    }

    public static ApiVersionSet GetApiVersionSet(this WebApplication webApplication)
    {
        return webApplication.NewApiVersionSet()
                  .HasApiVersion(new ApiVersion(1))
                  .ReportApiVersions()
                  .Build();
    }

    public static void ConfigureMiddleware(this WebApplication webApplication)
    {
        //if (!webApplication.Environment.IsDevelopment())
        //{
        webApplication.UseMiddleware<ExceptionHandlingMiddleware>();
        //}
    }

    //public static void ConfigureCors(this WebApplication webApplication)
    //{
    //    webApplication.UseCors(x => x
    //        .AllowAnyMethod()
    //        .AllowAnyHeader()
    //        .AllowCredentials()
    //        .WithOrigins(EnvironmentVariablesHelper.VueFrontEndBaseUrl,
    //                     EnvironmentVariablesHelper.NextJsFrontEndBaseUrl,
    //                     EnvironmentVariablesHelper.NextJsFrontEndAdminBaseUrl,
    //                     EnvironmentVariablesHelper.ProductionFrontEndBaseUrl));

    //}
}
