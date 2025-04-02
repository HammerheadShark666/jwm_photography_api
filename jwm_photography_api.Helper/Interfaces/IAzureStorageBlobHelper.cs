using Microsoft.AspNetCore.Http;

namespace jwm_photography_api.Helpers.Interfaces;

public interface IAzureStorageBlobHelper
{
    Task SaveBlobToAzureStorageContainerAsync(IFormFile file, string containerName, string fileName);
    Task SaveBlobsToAzureStorageContainerAsync(List<IFormFile> files, string containerName);
    Task DeleteBlobInAzureStorageContainerAsync(string fileName, string containerName);
}