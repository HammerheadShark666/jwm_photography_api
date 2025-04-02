namespace jwm_photography_api.Helper;
public class Base
{
    public Base() { }

    public string GetStorageConnection()
    {
        return EnvironmentVariables.AzureStorageConnectionString;
    }
}