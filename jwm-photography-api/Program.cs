using jwm_photography_api.Endpoints;
using jwm_photography_api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureApplicationInsights();
builder.Services.ConfigureMVC();
builder.Services.ConfigureControllers();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureDI();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureMediatR();
builder.Services.ConfigureExceptionHandling();
builder.Services.ConfigureJwtAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.ConfigureSwagger();

app.UseHttpsRedirection();
app.MapControllers();
app.ConfigureMiddleware();
app.UseAuthentication();
app.UseAuthorization();

EndpointsAuthenticate.ConfigureRoutes(app);
EndpointsCategory.ConfigureRoutes(app);
EndpointsCountry.ConfigureRoutes(app);
EndpointsPalette.ConfigureRoutes(app);
EndpointsGallery.ConfigureRoutes(app);
EndpointsPhoto.ConfigureRoutes(app);
EndpointsFavourite.ConfigureRoutes(app);

app.Run();