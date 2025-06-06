﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using jwm_photography_api.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;

namespace jwm_photography_api.Helper;

public class AzureStorageBlobHelper : Base, IAzureStorageBlobHelper
{
    public AzureStorageBlobHelper() : base()
    { }

    public async Task SaveBlobToAzureStorageContainerAsync(IFormFile file, string containerName, string fileName)
    {
        Stream fileStream;
        fileStream = file.OpenReadStream();
        var blobClient = new BlobContainerClient(GetStorageConnection(), containerName);
        var blob = blobClient.GetBlobClient(fileName);
        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = Constants.FileContentType });

        return;
    }

    public async Task SaveBlobsToAzureStorageContainerAsync(List<IFormFile> files, string containerName)
    {
        foreach (IFormFile file in files)
        {
            await SaveBlobToAzureStorageContainerAsync(file, containerName, file.FileName);
        }

        return;
    }

    public async Task DeleteBlobInAzureStorageContainerAsync(string fileName, string containerName)
    {
        if (fileName == null) return;

        BlobServiceClient blobServiceClient = new(GetStorageConnection());
        BlobContainerClient container = blobServiceClient.GetBlobContainerClient(containerName);
        await container.DeleteBlobIfExistsAsync(fileName);

        return;
    }
}