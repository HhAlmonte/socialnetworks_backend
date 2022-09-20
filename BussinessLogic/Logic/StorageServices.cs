using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Enums;
using Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BussinessLogic.Logic
{
    public class StorageServices : IStorageService
    {
        private readonly string azureStorageConnectionString;
        private readonly string firebaseStorageConnectionString;
        public StorageServices(IConfiguration configuration)
        {
            azureStorageConnectionString = configuration.GetConnectionString("AzureStorage");
            firebaseStorageConnectionString = configuration.GetConnectionString("FirebaseStorage");
        }
        
        // Azure services
        public async Task<string> AzureUploadAsync(IFormFile file, ContainerEnum container, string? blobName = null)
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
        public async Task AzureDeleteAsync(ContainerEnum container, string blobFileName)
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


        // Firebase services
        public Task<string> FirebaseUploadAsync(IFormFile file, ContainerEnum container, string? storageName = null)
        {
            throw new NotImplementedException();
        }

    }
}
