using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Interface
{
    public interface IStorageService
    {
        /// <summary>
        /// Upload file to Azure Blob Storage
        /// </summary>
        /// <param name="file"></param>
        /// <param name="container"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        Task<string> AzureUploadAsync(IFormFile file, ContainerEnum container, string? blobName = null);

        /// <summary>
        /// Delete file from Azure Blob Storage
        /// </summary>
        /// <param name="container"></param>
        /// <param name="blobFileName"></param>
        /// <returns></returns>
        Task AzureDeleteAsync(ContainerEnum container, string blobFileName);

        /// <summary>
        /// Upload file to Firebase Storage
        /// </summary>
        /// <param name="file"></param>
        /// <param name="container"></param>
        /// <param name="storageName"></param>
        /// <returns></returns>
        Task<string> FirebaseUploadAsync(IFormFile file, ContainerEnum container, string? storageName = null);
    }
}
