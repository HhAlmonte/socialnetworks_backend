using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Enums;
using Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BussinessLogic.Logic
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly string azureStorageConnectionString;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            azureStorageConnectionString = configuration.GetConnectionString("ConnectionContainer");
        }

        public async Task DeleteAsync(ContainerEnum container, string blobFileName)
        {
            var containerName =  Enum.GetName(typeof(ContainerEnum), container).ToLower();
            var blobContainerClient = new BlobContainerClient(azureStorageConnectionString,containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobFileName);

            try
            {
                await blobClient.DeleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UploadAsync(IFormFile file, ContainerEnum container, string? blobName = null)
        {
            if (file.Length == 0) return null;

            var containerName = Enum.GetName(typeof(ContainerEnum), container).ToLower();

            var blobContainerClient = new BlobContainerClient(azureStorageConnectionString, containerName);

            if (string.IsNullOrEmpty(blobName))
            {
                blobName = Guid.NewGuid().ToString();
            }

            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = file.ContentType };

            await using (Stream stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
            }

            return blobName;
        }
    }
}
