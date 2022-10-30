using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface ICloudinaryImageService
    {
        Task<ImageUploadResult> AddImageToCloudinaryAsync(IFormFile file);
        Task<DeletionResult> DeleteImageFromCloudinaryAsync(string publicId);
    }
}
