using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.Interface
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadAsync(IFormFile file, ContainerEnum container, string? blobName = null);

        Task DeleteAsync(ContainerEnum container, string blobFileName);
    }
}
