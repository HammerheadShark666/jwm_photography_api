// Ignore Spelling: Jwt

using jwm_photography_api.Helpers.Exceptions;

namespace jwm_photography_api.Helper;

public class EnvironmentVariables
{
    public static string AzureStorageUrl = GetEnvironmentVariable(Constants.AzureStorageUrl);

    public static string ApplicationInsightsConnectionString = GetEnvironmentVariable(Constants.ApplicationInsightsConnectionString);
    public static string AzureStorageConnectionString => GetEnvironmentVariable(Constants.AzureStorageConnectionString);
    public static string JwtIssuer => GetEnvironmentVariable(Constants.JwtIssuer);
    public static string JwtAudience => GetEnvironmentVariable(Constants.JwtAudience);
    public static string JwtSymmetricSecurityKey => GetEnvironmentVariable(Constants.JwtSymmetricSecurityKey);
    public static string AzureUserAssignedManagedIdentityClientId => GetEnvironmentVariable(Constants.AzureUserAssignedManagedIdentityClientId);
    public static string JwtSettingsSecret => GetEnvironmentVariable(Constants.JwtSettingsSecret);
    public static int JwtSettingsRefreshTokenTtl => Convert.ToInt16(GetEnvironmentVariable(Constants.JwtSettingsRefreshTokenTtl));
    public static int JwtSettingsTokenExpiryMinutes => Convert.ToInt16(GetEnvironmentVariable(Constants.JwtSettingsTokenExpiryMinutes));
    public static int JwtSettingsRefreshTokenExpiryDays => Convert.ToInt16(GetEnvironmentVariable(Constants.JwtSettingsRefreshTokenExpiryDays));
    public static int JwtSettingsPasswordTokenExpiryDays => Convert.ToInt16(GetEnvironmentVariable(Constants.JwtSettingsPasswordTokenExpiryDays));

    public static string GetEnvironmentVariable(string name)
    {
        var variable = Environment.GetEnvironmentVariable(name);

        if (string.IsNullOrEmpty(variable))
            throw new EnvironmentVariableNotFoundException($"Environment Variable Not Found: {name}.");

        return variable;
    }
}