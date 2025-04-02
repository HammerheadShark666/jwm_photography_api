// Ignore Spelling: Jwt

namespace jwm_photography_api.Helper;

public class Constants
{
    public const string JwtIssuer = "JWT_ISSUER";
    public const string JwtAudience = "JWT_AUDIENCE";
    public const string JwtSymmetricSecurityKey = "JWT_SYMMETRIC_SECURITY_KEY";

    public const string JwtSettingsSecret = "JWT_SETTINGS_SECRET";
    public const string JwtSettingsRefreshTokenTtl = "JWT_SETTINGS_REFRESH_TOKEN_TTL";
    public const string JwtSettingsTokenExpiryMinutes = "JWT_SETTINGS_TOKEN_EXPIRY_MINUTES";
    public const string JwtSettingsRefreshTokenExpiryDays = "JWT_SETTINGS_REFRESH_TOKEN_EXPIRY_DAYS";
    public const string JwtSettingsPasswordTokenExpiryDays = "JWT_SETTINGS_RESET_PASSWORD_TOKEN_EXPIRY_DAYS";

    public const string AzureUserAssignedManagedIdentityClientId = "AZURE_USER_ASSIGNED_MANAGED_IDENTITY_CLIENT_ID";
    public const string AzureDatabaseConnectionString = "AZURE_MANAGED_IDENTITY_SQL_CONNECTION";

    public const string DatabaseConnectionString = "SQLAZURECONNSTR_JWM_PHOTOGRAPHY";

    public const string AzureStorageConnectionString = "JWM_PHOTOGRAPHY_AZURE_STORAGE_CONNECTION_STRING";

    public const string ApplicationInsightsConnectionString = "APPLICATIONINSIGHTS_CONNECTION_STRING";

    public const string FileContentType = "image/jpeg";

    public const string AzureStorageUrl = "AZURE_STORAGE_URL";
}